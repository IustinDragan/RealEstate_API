using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DataAccess.Entities;

public class Announcement
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Property Property { get; set; }
    
    public ICollection<UserAnnouncement> UserAnnouncements { get; set; }
    
    public int? UserId { get; set; }
    
    public User? User { get; set; }

    public IEnumerable<UserAnnouncement> Users { get; set; }
}