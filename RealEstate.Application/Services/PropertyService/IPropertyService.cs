using RealEstate.Application.Models.PropertyModels;

namespace RealEstate.Application.Services.PropertyService;

public interface IPropertyService
{
    Task<IEnumerable<PropertyResponseModel>> GetPropertiesAsync();
    Task<PropertyResponseModel> CreatePropertyAsync(CreatePropertyRequestModel createPropertyRequestModel);
    Task<PropertyResponseModel?> GetPropertyByIdAsync(int id);
    Task<PropertyResponseModel> UpdatePropertyAsync(int id, UpdatePropertyRequestModel updateUsersRequestModel);
    void DeletePropertyAsync(int id);

    Task<bool> SaveChangesAsync();
}