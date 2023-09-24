using FluentValidation;
using RealEstate.Application.Models;

namespace RealEstate.Application.Validators;

public class CrreateUserRequestModelValidator : AbstractValidator<CreateUsersRequestModel>
{
    public CrreateUserRequestModelValidator()
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
            .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");

        RuleFor(u => u.isAgent)
            .NotNull().WithMessage("{PropertyName} field is required");

        RuleFor(u => u)
            .Cascade(CascadeMode.StopOnFirstFailure)
            .Must(model => !(model.isAgent && (string.IsNullOrWhiteSpace(model.Company?.CompanyName) ||
                                               string.IsNullOrWhiteSpace(model.Company?.CUI))))
            .WithMessage("When 'isAgent' is true, Company name and CUI fields are required.");
    }

    //add failure + error message: min 15:00 -> https://www.youtube.com/watch?v=Zh1ccvTFzs8

    protected bool BeAValidName(string name)
    {
        name = name.Replace(" ", "");
        name = name.Replace("-", "");

        return name.All(char.IsLetter);
    }

    //protected bool isUniqueEmail(string email) => !_userRepository.isEmailUnique();
}