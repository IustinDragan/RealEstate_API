using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DataAccess;

public class Adress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public int Floors { get; set; } //etaj
    public string Scale { get; set; } //scara
    public int AppartamentNumber { get; set; }
    public string GoogleMapsCoordinates { get; set; }

    [ForeignKey("PropertyId")] public Property? Property { get; set; }
    public int? PropertyId { get; set; }


    public override string ToString()
    {
        return
            $"Strada: {Street}, Numarul + {StreetNumber}, Tara: {Country}, Oras: {City}, Etaj: {Floors}, Scara: {Scale}";
    }
}