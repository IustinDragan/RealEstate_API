using FluentValidation;
using RealEstate.Application.Models.AdressModels;

namespace RealEstate.Application.Validators;

public class AddressRequestModelValidator : AbstractValidator<CreateAdressRequestModel>
{
    public AddressRequestModelValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        RuleFor(a => a.StreetNumber)
            .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        RuleFor(a => a.District)
            .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        RuleFor(a => a.City)
            .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
        RuleFor(a => a.Locality)
            .NotEmpty().WithMessage("{PropertyName} shouldn't be empty");
    }
}