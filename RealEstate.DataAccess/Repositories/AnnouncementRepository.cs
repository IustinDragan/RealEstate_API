using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Repositories.Interfaces;

namespace RealEstate.DataAccess.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly DatabaseContext _databaseContext;

    public AnnouncementRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }


    public async Task<Announcement> InsertAsync(Announcement announcement)
    {
        var addedEntity = await _databaseContext.Announcement.AddAsync(announcement);

        await _databaseContext.SaveChangesAsync();

        return addedEntity.Entity;
    }

    public async Task<Announcement> UpdateAsync(Announcement announcement)
    {
        var updatedEntity = _databaseContext
            .Announcement
            .Update(announcement);

        await _databaseContext.SaveChangesAsync();

        return updatedEntity.Entity;
    }

    public async Task<List<Announcement>> ReadAllAsync(string? orderBy, string? searchText, double? price,
        double? maxValue, int? roomsNumber, string? city,
        int page, int pageCount)
    {
        var orderByConfiguration = new Dictionary<string, Expression<Func<Announcement, object>>>
        {
            { "title", x => x.Title },
            { "startdate", x => x.StartDate },
            { "enddate", x => x.EndDate },
            { "roomsnumber", x => x.Property.RoomsNumber },
            { "bathroomsnumber", x => x.Property.BathroomsNumber },
            { "landAaea", x => x.Property.LandArea },
            { "houseusablearea", x => x.Property.HouseUsableArea },
            { "housetotalarea", x => x.Property.HouseTotalArea },
            { "constructionyear", x => x.Property.ConstructionYear },
            { "floorstotalnumber", x => x.Property.FloorsTotalNumber },
            { "apartamentfloor", x => x.Property.ApartamentFloor },
            { "elevator", x => x.Property.Elevator },
            { "price", x => x.Property.Price },
            // { "maxValue", x => x.Property.maxValue },
            { "description", x => x.Property.Details },
            { "propertytype", x => x.Property.PropertyType },
            { "houselanddetails", x => x.Property.HouseLandDetails },
            { "heatingsource", x => x.Property.HeatingSource },
            { "utilities", x => x.Property.Utilities },
            { "street", x => x.Property.Adress.Street },
            { "district", x => x.Property.Adress.District }, //judet
            { "city", x => x.Property.Adress.City }, //oras/comuna
            { "locality", x => x.Property.Adress.Locality }, //localitate/sat
            { "floors", x => x.Property.Adress.Floors }
        };

        var query = _databaseContext.Announcement.Include(a => a.Property).ThenInclude(p => p.Adress).AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
            query = query.Where(p => p.Title.Contains(searchText) || p.Property.Details.Contains(searchText));

        if (roomsNumber.HasValue)
            query = query.Where(p => p.Property.RoomsNumber == roomsNumber);

        if (!string.IsNullOrEmpty(city))
            query = query.Where(p => p.Property.Adress.City.Contains(city));

        if (price.HasValue && maxValue.HasValue)
        {
            query = query.Where(p => p.Property.Price >= price && p.Property.Price <= maxValue);
        }
        else
        {
            // if (price.HasValue )
            //     query = query.Where(p => p.Property.Price.Equals(price));
            if (price.HasValue)
                query = query.Where(p => p.Property.Price >= price);
            else if (maxValue.HasValue) query = query.Where(p => p.Property.Price <= maxValue);
        }

        if (!string.IsNullOrEmpty(orderBy))
        {
            if (!orderByConfiguration.ContainsKey(orderBy.ToLower()))
                throw new Exception($"Nu se poate ordona dupa {orderBy}");
            query = query.OrderByDescending(orderByConfiguration[orderBy]);
        }

        query = query.Skip((page - 1) * pageCount).Take(pageCount);

        return await query.ToListAsync();

        // var includableQuery = _databaseContext.Announcement
        //     .Include(a => a.Property)
        //     .ThenInclude(p => p.Adress);
        // IQueryable<Announcement> secondQuery = includableQuery.Where(x=> true);
        // IOrderedQueryable<Announcement> orderedQuery;

        // if (searchText != null)
        // {
        //     secondQuery = includableQuery.Where(a=>a.Title.Contains(searchText) || a.Property.Details.Contains(searchText));
        //     
        // }
        // if (orderBy != null)
        //     switch (orderBy)
        //     {
        //         case "startDate":
        //             orderedQuery  = secondQuery.OrderByDescending(x => x.StartDate);
        //             break;
        //         case "endDate":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.EndDate);
        //             break;
        //         case "city":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.Property.Adress.City);
        //             break;
        //         case "street":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.Property.Adress.Street);
        //             break;
        //         case "district":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.Property.Adress.District);
        //             break;
        //         case "roomsNumber":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.Property.RoomsNumber);
        //             break;
        //         case "bathroomsNumber":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.Property.BathroomsNumber);
        //             break;
        //         case "constructionYear":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.Property.ConstructionYear);
        //             break;
        //         case "price":
        //             orderedQuery = secondQuery.OrderByDescending(x => x.Property.Price);
        //             break;
        //         default:
        //             orderedQuery =secondQuery.OrderByDescending(x => x.Title);
        //             break;
        //     }

        // return await includableQuery.Skip((page - 1) * pageCount).Take(pageCount).ToListAsync();
    }

    public async Task<Announcement?> ReadByIdAsync(int id)
    {
        return await _databaseContext
            .Announcement
            .Where(x => x.Id == id)
            .Include(a => a.Property)
            .ThenInclude(p => p.Adress)
            .FirstOrDefaultAsync();
    }

    // public Task<List<Announcement>> SearchAnnouncements(string searchText)
    // {
    //     return _databaseContext.Announcement
    //         .Where(a => a.Title.Contains(searchText) || a.Property.Details.Contains(searchText)).ToListAsync();
    // }

    public async Task DeleteAsync(int id)
    {
        var announcement = await ReadByIdAsync(id);

        _databaseContext.Announcement.Remove(announcement);

        await _databaseContext.SaveChangesAsync();
    }
}