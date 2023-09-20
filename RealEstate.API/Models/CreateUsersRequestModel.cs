using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.Models
{
    public class CreateUsersRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool isAgent { get; set; }
        public CreateUserCompanyRequestModel Company { get; set; }
    }



    public class CreateUserCompanyRequestModel
    {
        public string CompanyName { get; set; }
        public string CUI { get; set; }
    }
}
