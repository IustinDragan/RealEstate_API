using RealEstate.DataAccess;

namespace RealEstate.Application.Models.AdressModels;

public class AdressResponseModel
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public int Floors { get; set; } //etaj
    public string Scale { get; set; } //scara
    public int AppartamentNumber { get; set; }

    public string GoogleMapsCoordinates { get; set; }
    //public Property Property { get; set; }

    public static AdressResponseModel FromAdress(Adress adress)
    {
        return new AdressResponseModel
        {
            Id = adress.Id,
            Street = adress.Street,
            StreetNumber = adress.StreetNumber,
            Country = adress.Country,
            City = adress.City,
            Floors = adress.Floors,
            Scale = adress.Scale,
            AppartamentNumber = adress.AppartamentNumber,
            GoogleMapsCoordinates = adress.GoogleMapsCoordinates
        };
    }
}