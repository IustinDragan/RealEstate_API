using RealEstate.DataAccess;
using RealEstate.DataAccess.Enums;

namespace RealEstate.Application.Models.PropertyModels;

public class PropertyResponseModel
{
    public int Id { get; set; }
    public int RoomsNumber { get; set; }
    public int BathroomsNumber { get; set; }
    public double LandArea { get; set; }
    public double HouseUsableArea { get; set; }
    public double HouseTotalArea { get; set; }
    public int ConstructionYear { get; set; }
    public int FloorsTotalNumber { get; set; }
    public int ApartamentFloor { get; set; }
    public bool Elevator { get; set; }
    public string Details { get; set; }
    public double Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public HouseLandDetails HouseLandDetails { get; set; }
    public HeatingSource HeatingSource { get; set; }
    public Utilities Utilities { get; set; }
    public int? AnnouncementId { get; set; }
    public string? Adress { get; set; }

    public static PropertyResponseModel FromProperty(Property property)
    {
        return new PropertyResponseModel
        {
            Id = property.Id,
            RoomsNumber = property.RoomsNumber,
            BathroomsNumber = property.BathroomsNumber,
            LandArea = property.LandArea,
            HouseUsableArea = property.HouseUsableArea,
            HouseTotalArea = property.HouseTotalArea,
            ConstructionYear = property.ConstructionYear,
            FloorsTotalNumber = property.FloorsTotalNumber,
            ApartamentFloor = property.ApartamentFloor,
            Elevator = property.Elevator,
            Details = property.Details,
            Price = property.Price,
            PropertyType = property.PropertyType,
            HouseLandDetails = property.HouseLandDetails,
            HeatingSource = property.HeatingSource,
            Utilities = property.Utilities,
            AnnouncementId = property.AnnouncementId,
            Adress = property.Adress?.ToString()
        };
    }

    // public static PropertyResponseModel FromPropertyForAnnouncement(Property property)
    // {
    //     return new PropertyResponseModel
    //     {
    //         
    //         Id = property.Id,
    //         RoomsNumber = property.RoomsNumber,
    //         BathroomsNumber = property.BathroomsNumber,
    //         LandArea = property.LandArea,
    //         HouseUsableArea = property.HouseUsableArea,
    //         HouseTotalArea = property.HouseTotalArea,
    //         ConstructionYear = property.ConstructionYear,
    //         FloorsTotalNumber = property.FloorsTotalNumber,
    //         ApartamentFloor = property.ApartamentFloor,
    //         Elevator = property.Elevator,
    //         Details = property.Details,
    //         Price = property.Price,
    //         PropertyType = property.PropertyType,
    //         HouseLandDetails = property.HouseLandDetails,
    //         HeatingSource = property.HeatingSource,
    //         Utilities = property.Utilities,
    //         AnnouncementId = property.AnnouncementId,
    //         //Adress = AdressResponseModel.FromAdress().ToString()
    //     };
    // }
}