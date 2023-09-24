using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.DataAccess.Users;

namespace RealEstate.DataAccess.Announcement;

public class UserAnnouncement
{
    
    [ForeignKey("UserId")]
    public User User { get; set; }
    public int UserId { get; set; }
    
    
    [ForeignKey("AnnouncementId")]
    public Announcement Announcement { get; set; }
    public int AnnouncementId { get; set; }
}