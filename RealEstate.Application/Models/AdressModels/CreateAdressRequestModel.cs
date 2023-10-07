using RealEstate.DataAccess;

namespace RealEstate.Application.Models.AdressModels;

public class CreateAdressRequestModel
{
    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Locality { get; set; }
    public int Floors { get; set; } //etaj
    public string Scale { get; set; } //scara
    public int AppartamentNumber { get; set; }


    public Adress toAdress()
    {
        return new Adress
        {
            Street = Street,
            StreetNumber = StreetNumber,
            District = District,
            City = City,
            Locality = Locality,
            Floors = Floors,
            Scale = Scale,
            AppartamentNumber = AppartamentNumber
        };
    }
}