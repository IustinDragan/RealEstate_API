using RealEstate.Application.Models.AdressModels;
using RealEstate.DataAccess;
using RealEstate.DataAccess.Enums;

namespace RealEstate.Application.Models.PropertyModels;

public class UpdatePropertyRequestModel
{
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
    public CreateAdressRequestModel Adress { get; set; }


    public Property ToPropertyUpdate()
    {
        return new Property
        {
            RoomsNumber = RoomsNumber,
            BathroomsNumber = BathroomsNumber,
            LandArea = LandArea,
            HouseUsableArea = HouseUsableArea,
            HouseTotalArea = HouseTotalArea,
            ConstructionYear = ConstructionYear,
            FloorsTotalNumber = FloorsTotalNumber,
            ApartamentFloor = ApartamentFloor,
            Elevator = Elevator,
            Details = Details,
            Price = Price,
            PropertyType = PropertyType,
            HouseLandDetails = HouseLandDetails,
            HeatingSource = HeatingSource,
            Utilities = Utilities,
            Adress = Adress.toAdress()
        };
    }
}