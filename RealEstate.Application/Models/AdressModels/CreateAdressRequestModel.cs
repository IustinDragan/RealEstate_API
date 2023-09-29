using RealEstate.DataAccess;

namespace RealEstate.Application.Models.AdressModels;

public class CreateAdressRequestModel
{
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public int Floors { get; set; } //etaj
    public string Scale { get; set; } //scara
    public int AppartamentNumber { get; set; }

    public string GoogleMapsCoordinates { get; set; }
    //public CreatePropertyRequestModel Property { get; set; }

    public Adress toAdress()
    {
        return new Adress
        {
            Street = Street,
            StreetNumber = StreetNumber,
            Country = Country,
            City = City,
            Floors = Floors,
            Scale = Scale,
            AppartamentNumber = AppartamentNumber,
            GoogleMapsCoordinates = GoogleMapsCoordinates
        };
    }
}