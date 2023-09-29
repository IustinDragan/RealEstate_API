using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Models.PropertyModels;
using RealEstate.DataAccess;

namespace RealEstate.Application.Services.PropertyService;

public class PropertyService : IPropertyService
{
    private readonly DatabaseContext _databaseContext;

    public PropertyService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext ?? throw new ArgumentNullException(nameof(databaseContext));
    }

    public async Task<IEnumerable<PropertyResponseModel>> GetPropertiesAsync()
    {
        var propertyFromDbQuery = _databaseContext.Property.Include(x => x.Adress);
        var propertyFromDb = await propertyFromDbQuery.Select(property => PropertyResponseModel.FromProperty(property))
            .ToListAsync();

        return propertyFromDb;
    }

    public async Task<PropertyResponseModel> CreatePropertyAsync(CreatePropertyRequestModel createPropertyRequestModel)
    {
        var property = createPropertyRequestModel.ToProperty();


        _databaseContext.Property.Add(property);
        await _databaseContext.SaveChangesAsync();

        return PropertyResponseModel.FromProperty(property);
    }

    public async Task<PropertyResponseModel?> GetPropertyByIdAsync(int id)
    {
        return await _databaseContext.Property.Where(p => p.Id == id).Include(c => c.Adress)
            .Select(property => PropertyResponseModel.FromProperty(property))
            .FirstOrDefaultAsync();
    }

    public async Task<PropertyResponseModel> UpdatePropertyAsync(int id,
        UpdatePropertyRequestModel updateUsersRequestModel)
    {
        var propertyFromDb = await _databaseContext.Property.Where(p => p.Id == id).Include(p => p.Adress).FirstAsync();
        if (propertyFromDb == null) throw new NullReferenceException("The property doesn't exist");

        _databaseContext.Attach(propertyFromDb);

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
        propertyFromDb.Adress = updateUsersRequestModel.AdressRequestModel.toAdress();

        await _databaseContext.SaveChangesAsync();

        var updatePropertyResponseModel = new PropertyResponseModel
        {
            Id = propertyFromDb.Id,
            RoomsNumber = propertyFromDb.RoomsNumber,
            BathroomsNumber = propertyFromDb.BathroomsNumber,
            LandArea = propertyFromDb.LandArea,
            HouseUsableArea = propertyFromDb.HouseUsableArea,
            HouseTotalArea = propertyFromDb.HouseTotalArea,
            ConstructionYear = propertyFromDb.ConstructionYear,
            FloorsTotalNumber = propertyFromDb.FloorsTotalNumber,
            ApartamentFloor = propertyFromDb.ApartamentFloor,
            Elevator = propertyFromDb.Elevator,
            Details = propertyFromDb.Details,
            Price = propertyFromDb.Price,
            PropertyType = propertyFromDb.PropertyType,
            HouseLandDetails = propertyFromDb.HouseLandDetails,
            HeatingSource = propertyFromDb.HeatingSource,
            Utilities = propertyFromDb.Utilities,
            AnnouncementId = propertyFromDb.AnnouncementId,
            Adress = propertyFromDb.Adress?.ToString()
        };

        return updatePropertyResponseModel;
    }

    public void DeletePropertyAsync(int id)
    {
        var propertyForDelete = _databaseContext.Property.Where(p => p.Id == id).Include(p => p.Adress).First();
        if (propertyForDelete == null)
            throw new NullReferenceException("The property doesn't exist");

        _databaseContext.Property.Remove(propertyForDelete);

        _databaseContext.SaveChanges();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _databaseContext.SaveChangesAsync() >= 0;
    }
}