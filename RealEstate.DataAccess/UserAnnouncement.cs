using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstate.DataAccess;

public class UserAnnouncement
{
    [ForeignKey("UserId")] public User User { get; set; }

    public int UserId { get; set; }


    [ForeignKey("AnnouncementId")] public Announcement Announcement { get; set; }

    public int AnnouncementId { get; set; }
}