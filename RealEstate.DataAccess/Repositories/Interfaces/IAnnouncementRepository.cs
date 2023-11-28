namespace RealEstate.DataAccess.Repositories.Interfaces;

public interface IAnnouncementRepository
{
    public Task<Announcement> InsertAsync(Announcement announcement);

    public Task<Announcement> UpdateAsync(Announcement announcement);

    Task<List<Announcement>> ReadAllAsync(string? orderBy, string? searchText, double? price, double? maxValue,
        int page, int pageCount);

    public Task<Announcement> ReadByIdAsync(int id);

    //Task<List<Announcement>> SearchAnnouncements(string searchText);

    public Task DeleteAsync(int id);
}