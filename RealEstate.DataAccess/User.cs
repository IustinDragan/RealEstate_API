using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RealEstate.DataAccess.Enums;

namespace RealEstate.DataAccess;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //pt autoincrement
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }

    [ForeignKey("CompanyId")] public Company? Company { get; set; }

    public int? CompanyId { get; set; } // cheie straina referinta la USER  

    public ICollection<UserAnnouncement> UserAnnouncements { get; set; } = new List<UserAnnouncement>();
}