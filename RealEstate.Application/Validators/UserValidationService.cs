using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models;

namespace RealEstate.Application.Validators
{
    public class UserValidationService
    {
        public IActionResult ValidateUserModel(CreateUsersRequestModel createUsersRequestModel)
        {
            UserValidator validationRules = new UserValidator();

            var result = validationRules.Validate(createUsersRequestModel);

            if (!result.IsValid)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    return new BadRequestObjectResult(failure);
                }
            }
            return null;
        }
    }
}
