using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models.AnnouncementModels;
using RealEstate.Application.Models.PropertyModels;
using RealEstate.DataAccess;

namespace RealEstate.Application.Services.AnnouncementService;

public class AnnouncementService : IAnnouncementService
{
    private readonly DatabaseContext _databaseContext;

    public AnnouncementService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext ?? throw new ArgumentException(nameof(databaseContext));
    }

    public async Task<AnnouncementResponseModel> CreateAnnouncementAsync(
        CreateAnnouncementRequestModel createAnnouncementRequestModel)
    {
        var announcement = createAnnouncementRequestModel.ToAnnouncement();

        _databaseContext.Announcement.Add(announcement);
        await _databaseContext.SaveChangesAsync();

        return AnnouncementResponseModel.FromAnnouncement(announcement);
    }

    public async Task<IEnumerable<AnnouncementResponseModel>> GetAnnouncementsAsync()
    {
        var announcementFromDbQuery = _databaseContext.Announcement
            .Include(a => a.Property)
            .ThenInclude(p => p.Adress);
        var announcementFromDb = await announcementFromDbQuery
            .Select(announcement => AnnouncementResponseModel.FromAnnouncement(announcement)).ToListAsync();

        return announcementFromDb;
    }

    public async Task<AnnouncementResponseModel?> GetAnnouncementByIdAsync(int id)
    {
        return await _databaseContext.Announcement.Where(a => a.Id == id)
            .Include(p => p.Property)
            .ThenInclude(a => a.Adress)
            .Select(announcement => AnnouncementResponseModel.FromAnnouncement(announcement))
            .FirstOrDefaultAsync();
    }

    public async Task<AnnouncementResponseModel> UpdateAnnouncementAsync(int id,
        UpdateAnnouncementRequestModel updateAnnouncementRequestModel)
    {
        var announcementFromDb = await _databaseContext.Announcement
            .Where(a => a.Id == id)
            .Include(p => p.Property)
            .ThenInclude(a => a.Adress)
            .FirstAsync();

        if (announcementFromDb == null) throw new NullReferenceException("The announcement doesn't exist");

        _databaseContext.Attach(announcementFromDb);


        announcementFromDb.Title = updateAnnouncementRequestModel.Title;
        announcementFromDb.Title = updateAnnouncementRequestModel.Title;
        announcementFromDb.StartDate = updateAnnouncementRequestModel.StartDate;
        announcementFromDb.EndDate = updateAnnouncementRequestModel.EndDate;
        announcementFromDb.Property = updateAnnouncementRequestModel.Property.ToProperty();

        await _databaseContext.SaveChangesAsync();

        var updateAnnouncementMResponseModel = new AnnouncementResponseModel
        {
            Id = announcementFromDb.Id, //daca nu il pun aici, primesc id 0
            Title = announcementFromDb.Title,
            StartDate = announcementFromDb.StartDate,
            EndDate = announcementFromDb.EndDate,
            Property = PropertyResponseModel.FromProperty(announcementFromDb.Property)
        };

        return updateAnnouncementMResponseModel;
    }

    public void DeleteAnnouncementAsync(int id)
    {
        var announcementForDelete = _databaseContext.Announcement
            .Where(a => a.Id == id)
            .Include(p => p.Property)
            .ThenInclude(a => a.Adress)
            .First();

        if (announcementForDelete == null)
            throw new NullReferenceException("Announcement doesn't exist");

        _databaseContext.Announcement.Remove(announcementForDelete);
        _databaseContext.SaveChanges();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _databaseContext.SaveChangesAsync() >= 0;
    }
}