using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.API.Utility;
using RealEstate.Application.Exceptions;
using RealEstate.Application.Models.UsersModels;
using RealEstate.Application.Services.Interfaces;
using RealEstate.Application.Validators;
using RealEstate.Shared.Models.Users;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("users")]
[Authorize(Roles = "SalesAgent")]
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
    [AllowAnonymous]
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

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginRequestModel requestModel)
    {
        try
        {
            var response = await _userService.LoginAsync(requestModel);

            return Ok(response);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var userEntities = await _userService.RealAllUsersAsync();

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="announcementId">Announcement id</param>
    /// <returns></returns>
    [HttpPut("{userId}/favorites/{announcementId}")]
    public async Task<IActionResult> AddToFavorite(int userId, int announcementId)
    {
        var response = await _userService.AddAnnouncementToFavoriteAsync(userId, announcementId);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsersByIdAsync(int id)
    {
        await _userService.DeleteUserAsync(id);

        return NoContent();
    }
}