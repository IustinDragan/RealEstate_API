using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Models.AnnouncementModels;
using RealEstate.Application.Services.AnnouncementService;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("announcement")]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnnouncementAsync(
        CreateAnnouncementRequestModel createAnnouncementRequestModel)
    {
        var announcementEntity = await _announcementService.CreateAnnouncementAsync(createAnnouncementRequestModel);

        return Created("", announcementEntity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnnouncementsAsync()
    {
        var announcementEntity = await _announcementService.GetAnnouncementsAsync();

        return Ok(announcementEntity);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnnouncementByIdAsync(int id)
    {
        var announcementEntityById = await _announcementService.GetAnnouncementByIdAsync(id);

        return Ok(announcementEntityById);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnnouncementAsync(int id,
        UpdateAnnouncementRequestModel updateAnnouncementRequestModel)
    {
        var announcementEntityByIdForUpdate =
            await _announcementService.UpdateAnnouncementAsync(id, updateAnnouncementRequestModel);

        return Ok(announcementEntityByIdForUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnnouncementById(int id)
    {
        _announcementService.DeleteAnnouncementAsync(id);
        await _announcementService.SaveChangesAsync();

        return NoContent();
    }
}