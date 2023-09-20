using RealEstate.API.DataAccess.Users;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RealEstate.API.Models
{
    public class CreateUsersResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public Role Role { get; set; }
    }
}
