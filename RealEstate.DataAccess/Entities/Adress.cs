using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DataAccess.Entities;

public class Adress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Street { get; set; }
    public int StreetNumber { get; set; }
    public string District { get; set; } //judet
    public string City { get; set; } //oras/comuna
    public string Locality { get; set; } //localitate/sat
    public int Floors { get; set; } //etaj
    public string Scale { get; set; } //scara
    public int AppartamentNumber { get; set; }

    [ForeignKey("PropertyId")] 
    public Property? Property { get; set; }
    public int? PropertyId { get; set; }


    public override string ToString()
    {
        return
            $"Strada: {Street}, Numarul {StreetNumber}, Tara: {District}, Oras: {City}, Localitate: {Locality}, Etaj: {Floors}, Scara: {Scale}";
    }
}