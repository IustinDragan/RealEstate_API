using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models;
using RealEstate.API.Utility;
using RealEstate.Application.Services.Users;
using RealEstate.Application.Validators;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<CreateUsersRequestModel> _validator;
        private readonly UserValidationService _userValidationService;
        //private readonly UserValidator _userValidatorRules = new UserValidator();

        public UsersController(IUserRepository userRepository, IValidator<CreateUsersRequestModel> validator, UserValidationService userValidationService)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this._validator = validator;
            _userValidationService = userValidationService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUsersAsync(CreateUsersRequestModel createUsersRequestModel)
        {
            var validationResponse = _userValidationService.ValidateUserModel(createUsersRequestModel);

            if (validationResponse != null)
                return validationResponse;
            

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

        [HttpGet("id/{id:int}")]
        public async Task<IActionResult> GetUserByIdAsync(int id, bool includeCompanyDetails)
        {
            var userEntityById = await _userRepository.GetUserByIdAsync(id, includeCompanyDetails);

            return Ok(userEntityById);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetUserByName(string name, bool includeCompanyDetails)
        {
            var userEntityByName = await _userRepository.GetUserByNameAsync(name, includeCompanyDetails);

            return Ok(userEntityByName);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, CreateUsersRequestModel createUsersRequestModel)
        {
            var validationResponse = _userValidationService.ValidateUserModel(createUsersRequestModel);

            if (validationResponse != null)
                return validationResponse;

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
