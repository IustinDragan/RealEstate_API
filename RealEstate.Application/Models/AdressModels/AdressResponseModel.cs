using RealEstate.DataAccess;
using RealEstate.DataAccess.Entities;

namespace RealEstate.Application.Models.AdressModels;

public class AdressResponseModel
{
    public int Id { get; set; }
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Locality { get; set; }
    public int Floors { get; set; } //etaj
    public string Scale { get; set; } //scara
    public int AppartamentNumber { get; set; }

    public static AdressResponseModel FromAdress(Adress adress)
    {
        return new AdressResponseModel
        {
            Id = adress.Id,
            Street = adress.Street,
            StreetNumber = adress.StreetNumber,
            District = adress.District,
            City = adress.City,
            Locality = adress.Locality,
            Floors = adress.Floors,
            Scale = adress.Scale,
            AppartamentNumber = adress.AppartamentNumber
        };
    }
}