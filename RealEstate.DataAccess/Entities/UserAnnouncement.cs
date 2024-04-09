using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DataAccess.Entities;

[Table("UsersAnnouncements")]
public class UserAnnouncement
{
    public User User { get; set; }

    public int UserId { get; set; }

    public Announcement Announcement { get; set; }

    public int AnnouncementId { get; set; }
}
