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

    public async Task<List<Announcement>> ReadAllAsync(string? orderBy, int page, int pageCount)
    {
        var query = _databaseContext.Announcement
            .Include(a => a.Property)
            .ThenInclude(p => p.Adress).OrderBy(x => x.Title);

        if (orderBy != null)
            switch (orderBy)
            {
                case "startDate":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.StartDate);
                    break;
                case "endDate":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.EndDate);
                    break;
                case "city":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Property.Adress.City);
                    break;
                case "street":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Property.Adress.Street);
                    break;
                case "district":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Property.Adress.District);
                    break;
                case "roomsNumber":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Property.RoomsNumber);
                    break;
                case "bathroomsNumber":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Property.BathroomsNumber);
                    break;
                case "constructionYear":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Property.ConstructionYear);
                    break;
                case "price":
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Property.Price);
                    break;
                default:
                    query = _databaseContext.Announcement.Include(p => p.Property).ThenInclude(a => a.Adress)
                        .OrderByDescending(x => x.Title);
                    break;
            }

        return await query.Skip((page - 1) * pageCount).Take(pageCount).ToListAsync();
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

    public async Task DeleteAsync(int id)
    {
        var announcement = await ReadByIdAsync(id);

        _databaseContext.Announcement.Remove(announcement);

        await _databaseContext.SaveChangesAsync();
    }
}