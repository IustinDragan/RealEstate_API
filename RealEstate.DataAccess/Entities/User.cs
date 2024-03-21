using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.DataAccess.Enums;

namespace RealEstate.DataAccess.Entities;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //pt autoincrement
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? UserName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }

    public int? CompanyId { get; set; } // cheie straina referinta la USER  

    public Company? Company { get; set; }

    public ICollection<Announcement> Announcements { get; set; }
    
    public ICollection<UserAnnouncement> FavoritesAnnouncements { get; set; }
}