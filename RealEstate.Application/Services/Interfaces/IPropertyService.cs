using RealEstate.Application.Models.AnnouncementModels;
using RealEstate.Application.Models.PropertyModels;

namespace RealEstate.Application.Services.Interfaces;

public interface IPropertyService
{
    Task<IEnumerable<PropertyResponseModel>> GetPropertiesAsync(ReadPropertyRequestModel requestModel);
    Task<PropertyResponseModel> CreatePropertyAsync(CreatePropertyRequestModel createPropertyRequestModel);
    Task<PropertyResponseModel?> GetPropertyByIdAsync(int id);
    Task<PropertyResponseModel> UpdatePropertyAsync(int id, UpdatePropertyRequestModel updateUsersRequestModel);
    Task DeletePropertyAsync(int id);
}