using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Models.AdressModels;
using RealEstate.Application.Models.AnnouncementModels;
using RealEstate.Application.Models.PropertyModels;
using RealEstate.Application.Services.Interfaces;
using RealEstate.Application.Validators;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("properties")]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IValidator<CreateAdressRequestModel> _validator;

    public PropertiesController(IPropertyService propertyService,
        IValidator<CreateAdressRequestModel> validator)
    {
        _propertyService = propertyService ?? throw new ArgumentNullException(nameof(propertyService));
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePropertyAsync(CreatePropertyRequestModel createPropertyRequestModel)
    {
        var validationResponse = _validator.GetValidationResult(createPropertyRequestModel.Adress);
        if (validationResponse != null)
            return validationResponse;

        var createPropertyEntity = await _propertyService.CreatePropertyAsync(createPropertyRequestModel);

        return Created("", createPropertyEntity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPropertiesAsync([FromQuery] ReadPropertyRequestModel requestModel)
    {
        var propertyEntity = await _propertyService.GetPropertiesAsync(requestModel);

        return Ok(propertyEntity);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPropertyByIdAsync(int id)
    {
        var propertyEntityById = await _propertyService.GetPropertyByIdAsync(id);

        return Ok(propertyEntityById);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePropertyAsync(int id, UpdatePropertyRequestModel updatePropertyRequestModel)
    {
        var validationResponse = _validator.GetValidationResult(updatePropertyRequestModel.Adress);

        if (validationResponse != null)
            return validationResponse;

        var propertyEntityByIdForUpdate =
            await _propertyService.UpdatePropertyAsync(id, updatePropertyRequestModel);

        return Ok(propertyEntityByIdForUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropertyById(int id)
    {
        await _propertyService.DeletePropertyAsync(id);

        return NoContent();
    }
}