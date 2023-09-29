using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Models.PropertyModels;
using RealEstate.Application.Services.PropertyService;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("properties")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService ?? throw new ArgumentNullException(nameof(propertyService));
    }

    [HttpPost]
    public async Task<IActionResult> CreatePropertyAsync(CreatePropertyRequestModel createPropertyRequestModel)
    {
        var createPropertyEntity = await _propertyService.CreatePropertyAsync(createPropertyRequestModel);

        return Created("", createPropertyEntity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPropertiesAsync()
    {
        var propertyEntity = await _propertyService.GetPropertiesAsync();

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
        var propertyEntityByIdForUpdate = await _propertyService.UpdatePropertyAsync(id, updatePropertyRequestModel);

        return Ok(propertyEntityByIdForUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePropertyById(int id)
    {
        _propertyService.DeletePropertyAsync(id);
        await _propertyService.SaveChangesAsync();

        return NoContent();
    }
}