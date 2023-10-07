namespace RealEstate.DataAccess.Repositories.Interfaces;

public interface IAnnouncementRepository
{
    public Task<Announcement> InsertAsync(Announcement announcement);

    public Task<Announcement> UpdateAsync(Announcement announcement);

    Task<List<Announcement>> ReadAllAsync(string? orderBy, int page, int pageCount);

    public Task<Announcement> ReadByIdAsync(int id);

    public Task DeleteAsync(int id);
}