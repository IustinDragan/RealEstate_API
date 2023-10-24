using RealEstate.Web.Models.Enums;

namespace RealEstate.Web.Models;

public class CreatePropertyRequestModel
{
    public CreatePropertyRequestModel()
    {
        Adress = new CreateAdressRequestModel();
    }

    public int RoomsNumber { get; set; }
    public int BathroomsNumber { get; set; }
    public double LandArea { get; set; }
    public double HouseUsableArea { get; set; }
    public double HouseTotalArea { get; set; }
    public int ConstructionYear { get; set; }
    public int FloorsTotalNumber { get; set; }
    public int ApartamentFloor { get; set; }
    public bool Elevator { get; set; }
    public string? Details { get; set; }
    public double Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public HouseLandDetails HouseLandDetails { get; set; }
    public HeatingSource HeatingSource { get; set; }
    public Utilities Utilities { get; set; }
    public CreateAdressRequestModel Adress { get; set; }
}