namespace RealEstate.Web.Models;

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

    public override string ToString()
    {
        return
            $"Strada: {Street}, Numarul {StreetNumber}, Tara: {District}, Oras: {City}, Etaj: {Floors}, Scara: {Scale}";
    }
}