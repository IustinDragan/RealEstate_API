using RealEstate.Application.Models.AnnouncementModels;
using RealEstate.Application.Models.PropertyModels;
using RealEstate.Application.Services.Interfaces;
using RealEstate.DataAccess.Repositories.Interfaces;

namespace RealEstate.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository ?? throw new ArgumentNullException(nameof(propertyRepository));
    }

    public async Task<IEnumerable<PropertyResponseModel>>
        GetPropertiesAsync(ReadPropertyRequestModel requestModel) //ReadAllAsync
    {
        var property =
            await _propertyRepository.ReadAllAsync(requestModel.OrderBy, requestModel.page, requestModel.PageCount);

        return property.Select(PropertyResponseModel.FromProperty).ToList();
    }

    public async Task<PropertyResponseModel>
        CreatePropertyAsync(CreatePropertyRequestModel createPropertyRequestModel) //createAsync
    {
        var property = createPropertyRequestModel.ToProperty();

        var addedProperty = await _propertyRepository.InsertAsync(property);

        return PropertyResponseModel.FromProperty(addedProperty);
    }

    public async Task<PropertyResponseModel?> GetPropertyByIdAsync(int id)
    {
        var property = await _propertyRepository.ReadByIdAsync(id);

        return PropertyResponseModel.FromProperty(property);
    }

    public async Task<PropertyResponseModel> UpdatePropertyAsync(int id,
        UpdatePropertyRequestModel updateUsersRequestModel)
    {
        var propertyFromDb = await _propertyRepository.ReadByIdAsync(id);

        propertyFromDb.RoomsNumber = updateUsersRequestModel.RoomsNumber;
        propertyFromDb.BathroomsNumber = updateUsersRequestModel.BathroomsNumber;
        propertyFromDb.LandArea = updateUsersRequestModel.LandArea;
        propertyFromDb.HouseUsableArea = updateUsersRequestModel.HouseUsableArea;
        propertyFromDb.HouseTotalArea = updateUsersRequestModel.HouseTotalArea;
        propertyFromDb.ConstructionYear = updateUsersRequestModel.ConstructionYear;
        propertyFromDb.FloorsTotalNumber = updateUsersRequestModel.FloorsTotalNumber;
        propertyFromDb.ApartamentFloor = updateUsersRequestModel.ApartamentFloor;
        propertyFromDb.Elevator = updateUsersRequestModel.Elevator;
        propertyFromDb.Details = updateUsersRequestModel.Details;
        propertyFromDb.Price = updateUsersRequestModel.Price;
        propertyFromDb.PropertyType = updateUsersRequestModel.PropertyType;
        propertyFromDb.HouseLandDetails = updateUsersRequestModel.HouseLandDetails;
        propertyFromDb.HeatingSource = updateUsersRequestModel.HeatingSource;
        propertyFromDb.Utilities = updateUsersRequestModel.Utilities;
        propertyFromDb.Adress = updateUsersRequestModel.Adress.toAdress();


        await _propertyRepository.UpdateAsync(propertyFromDb);

        return PropertyResponseModel.FromProperty(propertyFromDb);
    }

    public async Task DeletePropertyAsync(int id)
    {
        await _propertyRepository.DeleteAsync(id);
    }
}