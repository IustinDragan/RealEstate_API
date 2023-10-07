using Microsoft.EntityFrameworkCore;
using RealEstate.DataAccess.Repositories.Interfaces;

namespace RealEstate.DataAccess.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly DatabaseContext _databaseContext;

    public PropertyRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Property> InsertAsync(Property property)
    {
        var addedEntity = await _databaseContext.Property.AddAsync(property);

        await _databaseContext.SaveChangesAsync();

        return addedEntity.Entity;
    }

    public async Task<Property> UpdateAsync(Property property)
    {
        var updatedEntity = _databaseContext.Property.Update(property);

        await _databaseContext.SaveChangesAsync();

        return updatedEntity.Entity;
    }

    public async Task<List<Property>> ReadAllAsync(string? orderBy, int page, int pageCount)
    {
        var query = _databaseContext.Property.Include(a => a.Adress).OrderBy(x => x.Price);

        if (orderBy != null)
            switch (orderBy)
            {
                case "city":
                    query = _databaseContext.Property.Include(a => a.Adress).OrderByDescending(x => x.Adress.City);
                    break;
                case "street":
                    query = _databaseContext.Property.Include(a => a.Adress).OrderByDescending(x => x.Adress.Street);
                    break;
                case "district":
                    query = _databaseContext.Property.Include(a => a.Adress).OrderByDescending(x => x.Adress.District);
                    break;
                case "roomsNumber":
                    query = _databaseContext.Property.Include(a => a.Adress).OrderByDescending(x => x.RoomsNumber);
                    break;
                case "bathroomsNumber":
                    query = _databaseContext.Property.Include(a => a.Adress).OrderByDescending(x => x.BathroomsNumber);
                    ;
                    break;
                case "constructionYear":
                    query = _databaseContext.Property.Include(a => a.Adress).OrderByDescending(x => x.ConstructionYear);
                    break;
                default:
                    query = _databaseContext.Property.Include(a => a.Adress).OrderByDescending(x => x.Price);
                    break;
            }

        return await query.Skip((page - 1) * pageCount).Take(pageCount).ToListAsync();
    }

    public async Task<Property?> ReadByIdAsync(int id)
    {
        return await _databaseContext
            .Property
            .Where(x => x.Id == id)
            .Include(a => a.Adress)
            .FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var property = await ReadByIdAsync(id);

        _databaseContext.Property.Remove(property);

        await _databaseContext.SaveChangesAsync();
    }
}