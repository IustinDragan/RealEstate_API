using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.DataAccess.Users
{
    public class Company
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //pt autoincrement
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CUI { get; set; }
    }
}
