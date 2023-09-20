using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Models;
using RealEstate.API.Services;
using RealEstate.API.Utility;

namespace RealEstate.API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }


        [HttpPost]
        public async Task<IActionResult> CreateUsersAsync(CreateUsersRequestModel createUsersRequestModel)
        {
            if (createUsersRequestModel.isAgent)
            {
                if(createUsersRequestModel.Company.CompanyName == null )
                {
                    return BadRequest("You need to specify the Company name");
                }
                else if(createUsersRequestModel.Company.CUI == null)
                {
                    return BadRequest("You need to specify the Company CUI");
                }
            }

            if (await _userRepository.isEmailUnique(createUsersRequestModel.Email))
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

            if (await _userRepository.isEmailUnique(createUsersRequestModel.Email))
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
