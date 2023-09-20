using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.DataAccess.Users
{
    public class User
    {
        public User() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //pt autoincrement
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
        
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        public int? CompanyId { get; set; } // cheie straina referinta la USER  
    }

    public enum Role
    {
        SalesAgent = 1,
        Customer = 2,
    }
}

