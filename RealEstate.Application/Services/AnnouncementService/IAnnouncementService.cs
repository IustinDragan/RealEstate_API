using RealEstate.Application.Models.AnnouncementModels;

namespace RealEstate.Application.Services.AnnouncementService;

public interface IAnnouncementService
{
    Task<IEnumerable<AnnouncementResponseModel>> GetAnnouncementsAsync();
    Task<AnnouncementResponseModel?> GetAnnouncementByIdAsync(int id);

    Task<AnnouncementResponseModel> CreateAnnouncementAsync(
        CreateAnnouncementRequestModel createAnnouncementRequestModel);

    Task<AnnouncementResponseModel> UpdateAnnouncementAsync(int id,
        UpdateAnnouncementRequestModel updateAnnouncementRequestModel);

    void DeleteAnnouncementAsync(int id);

    Task<bool> SaveChangesAsync();
}