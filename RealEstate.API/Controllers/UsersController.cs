using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Utility;
using RealEstate.Application.Models;
using RealEstate.Application.Services.Users;
using RealEstate.Application.Validators;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUsersRequestModel> _validator;

    public UsersController(IUserService userService,
        IValidator<CreateUsersRequestModel> validator)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUsersAsync(CreateUsersRequestModel createUsersRequestModel)
    {
        var validationResponse = _validator.GetValidationResult(createUsersRequestModel);

        if (validationResponse != null)
            return validationResponse;

        if (!await _userService.isEmailUniqueAsync(createUsersRequestModel.Email))
            return BadRequest(ErrorMessages.EmailAlreadyAssociatedWithAnAccount);

        var createUserEntity = await _userService.CreateUserAsync(createUsersRequestModel);

        return Created("", createUserEntity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var userEntities = await _userService.GetUsersAsync();

        return Ok(userEntities);
    }

    [HttpGet("id/{id:int}")]
    public async Task<IActionResult> GetUserByIdAsync(int id, bool includeCompanyDetails)
    {
        var userEntityById = await _userService.GetUserByIdAsync(id, includeCompanyDetails);

        return Ok(userEntityById);
    }

    [HttpGet("name/{name}")]
    public async Task<IActionResult> GetUserByName(string name, bool includeCompanyDetails)
    {
        var userEntityByName = await _userService.GetUserByNameAsync(name, includeCompanyDetails);

        return Ok(userEntityByName);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult>
        UpdateUserAsync(int id, CreateUsersRequestModel createUsersRequestModel) //min 11, ToDo base model 
    {
        var validationResponse = _validator.GetValidationResult(createUsersRequestModel);

        if (validationResponse != null)
            return validationResponse;

        if (!await _userService.isEmailUniqueAsync(createUsersRequestModel.Email))
            return BadRequest(ErrorMessages.EmailAlreadyAssociatedWithAnAccount);

        var userEntityByIdforUpdate = await _userService.UpdateUserAsync(id, createUsersRequestModel);

        return Ok(userEntityByIdforUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsersByIdAsync(int id)
    {
        _userService.DeleteUserAsync(id);
        await _userService.SaveChangesAsync();

        return NoContent();
    }
}