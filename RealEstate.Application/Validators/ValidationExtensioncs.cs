using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace RealEstate.Application.Validators;

public static class ValidationExtension
{
    public static IActionResult GetValidationResult<T>(this IValidator<T> validator, T model)
    {
        var result = validator.Validate(model);

        if (!result.IsValid)
            foreach (var failure in result.Errors)
                return new BadRequestObjectResult(failure);
        return null;
    }
}