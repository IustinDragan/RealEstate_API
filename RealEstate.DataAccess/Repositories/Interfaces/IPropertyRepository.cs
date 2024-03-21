using RealEstate.DataAccess.Entities;

namespace RealEstate.DataAccess.Repositories.Interfaces;

public interface IPropertyRepository
{
    public Task<Property> InsertAsync(Property announcement);

    public Task<Property> UpdateAsync(Property announcement);

    Task<List<Property>> ReadAllAsync(string? orderBy, int page, int pageCount);

    public Task<Property> ReadByIdAsync(int id);

    public Task DeleteAsync(int id);
}