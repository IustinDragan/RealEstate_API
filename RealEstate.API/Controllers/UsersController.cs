using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.DataAccess.Users;
using RealEstate.API.Models;
using RealEstate.API.Services;
using RealEstate.API.Utility;
using RealEstate.Application.Validators;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUsersRequestModel> _validator;

        public UsersController(IUserRepository userRepository, IValidator<CreateUsersRequestModel> validator)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this._validator = validator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUsersAsync(CreateUsersRequestModel createUsersRequestModel)
        {
            UserValidator validationRules = new UserValidator();

            var result = validationRules.Validate(createUsersRequestModel);

            if (!result.IsValid)
            {
                foreach (ValidationFailure failure in result.Errors)
                {
                    return BadRequest(failure);
                }
            }


            if (!(await _userRepository.isEmailUnique(createUsersRequestModel.Email)))
                return BadRequest(ErrorMessages.EmailAlreadyAssociatedWithAnAccount);

            var createUserEntity = await _userRepository.CreateUserAsync(createUsersRequestModel);
            return Created("", createUserEntity); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var userEntities = await _userRepository.GetUsersAsync();

            return Ok(userEntities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id, bool includeCompanyDetails)
        {
            var userEntityById = await _userRepository.GetUserByIdAsync(id, includeCompanyDetails);

            return Ok(userEntityById);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, CreateUsersRequestModel createUsersRequestModel)
        {

            if (createUsersRequestModel.isAgent)
            {
                if (createUsersRequestModel.Company.CompanyName == null)
                {
                    return BadRequest("You need to specify the Company name");
                }
                else if (createUsersRequestModel.Company.CUI == null)
                {
                    return BadRequest("You need to specify the Company CUI");
                }
            }

            if (!(await _userRepository.isEmailUnique(createUsersRequestModel.Email)))
                return BadRequest(ErrorMessages.EmailAlreadyAssociatedWithAnAccount);

            var userEntityByIdforUpdate = await _userRepository.UpdateUserAsync(id, createUsersRequestModel);

            return Ok(userEntityByIdforUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersByIdAsync(int id)
        {

            _userRepository.DeleteUserAsync(id);
            await _userRepository.SaveChangesAsync();

            return NoContent();
        } 
    }
}
