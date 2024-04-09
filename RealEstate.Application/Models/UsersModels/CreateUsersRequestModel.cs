using System.ComponentModel.DataAnnotations;
using RealEstate.DataAccess;
using RealEstate.DataAccess.Entities;

namespace RealEstate.Application.Models.UsersModels;

public class CreateUsersRequestModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    [EmailAddress] public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool isAgent { get; set; }
    public CreateUserCompanyRequestModel Company { get; set; }

    public User ToUser()
    {
        return new User
        {
            FirstName = FirstName,
            LastName = LastName,
            UserName = UserName,
            Email = Email,
            Password = Password,
            PhoneNumber = PhoneNumber,
            Company = !isAgent
                ? null
                : new Company
                {
                    CompanyName = Company.CompanyName,
                    CUI = Company.CUI
                }
        };
    }
}