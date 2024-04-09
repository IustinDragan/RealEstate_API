using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.DataAccess.Enums;

namespace RealEstate.DataAccess.Entities;

public class Property
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

    // public double? maxValue { get; set; }
    public PropertyType PropertyType { get; set; }
    public HouseLandDetails HouseLandDetails { get; set; }
    public HeatingSource HeatingSource { get; set; }
    public Utilities Utilities { get; set; }

    [ForeignKey("AnnouncementId")] public int? AnnouncementId { get; set; }

    public Announcement? Announcement { get; set; }
    public Adress Adress { get; set; }
}