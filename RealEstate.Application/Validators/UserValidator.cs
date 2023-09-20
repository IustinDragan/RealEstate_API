using FluentValidation;
using RealEstate.API.Models;
using RealEstate.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.Validators
{

    public class UserValidator : AbstractValidator<CreateUsersRequestModel>
    {


        public UserValidator() 
        {

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("{PropertyName} shouldn't be empty")
                .Must(BeAValidName).WithMessage("{PropertyName} contains invalid characters");
            
            RuleFor(u => u.Email)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .EmailAddress().WithMessage("The format is not correct")
                .NotEmpty().WithMessage("{PropertyName} adress shouldn't be empty");
            
            RuleFor(u => u.PhoneNumber)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .Length(10).WithMessage("{PropertyName} should be 10 characters")
                .NotEmpty().WithMessage("{PropertyName} is required");
            
            RuleFor(u => u.Password)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .MinimumLength(7).WithMessage("{PropertyName} should have at least 7 characters")
                .NotEmpty().WithMessage("{PropertyName} shouldn't ne empty");
            

            RuleFor(u => u.isAgent)
                       .NotEmpty().WithMessage("{PropertyName} field is required");

            RuleFor(u => u)
                .Must((model, _) => model.isAgent && (model.Company == null || model.Company.CompanyName == null))
                .WithMessage("When 'isAgent' is true, Company name field is required.");

            RuleFor(u => u)
                .Must((model, _) => model.isAgent && (model.Company == null || model.Company.CUI == null))
                .WithMessage("When 'isAgent' is true, CUI field is required.");
        }

        //add failure + error message: min 15:00 -> https://www.youtube.com/watch?v=Zh1ccvTFzs8

        


        protected bool BeAValidName(string name)
        {
            name = name.Replace(" ", "");
            name = name.Replace("-", "");

            return name.All(char.IsLetter);
        }

        //protected bool isUniqueEmail(string email) => !_userRepository.isEmailUnique(email);
    }
}
