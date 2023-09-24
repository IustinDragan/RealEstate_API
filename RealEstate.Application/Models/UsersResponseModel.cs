﻿using System.ComponentModel.DataAnnotations;
using RealEstate.DataAccess.Users;

namespace RealEstate.Application.Models;

public class UsersResponseModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress] public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }

    public static UsersResponseModel FromUser(User user)
    {
        return new UsersResponseModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailAddress = user.Email,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role
        };
    }
}