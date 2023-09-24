using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DataAccess.Announcement;

public class Announcement
{
    public Announcement(){}
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<UserAnnouncement> UserAnnouncements = new List<UserAnnouncement>();
}