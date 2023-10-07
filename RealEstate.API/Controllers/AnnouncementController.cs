﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Models.AdressModels;
using RealEstate.Application.Models.AnnouncementModels;
using RealEstate.Application.Services.Interfaces;
using RealEstate.Application.Validators;

namespace RealEstate.API.Controllers;

[ApiController]
[Route("announcement")]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;
    private readonly IValidator<CreateAdressRequestModel> _validator;

    public AnnouncementController(IAnnouncementService announcementService,
        IValidator<CreateAdressRequestModel> validator)
    {
        _announcementService = announcementService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAnnouncementAsync(
        CreateAnnouncementRequestModel createAnnouncementRequestModel)
    {
        var validationResponse = _validator.GetValidationResult(createAnnouncementRequestModel.Property.Adress);
        if (validationResponse != null)
            return validationResponse;


        var announcementEntity = await _announcementService.CreateAsync(createAnnouncementRequestModel);

        return Created("", announcementEntity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnnouncementsAsync([FromQuery] ReadAnnouncementRequestModel requestModel)
    {
        var announcementEntity = await _announcementService.RealAllAsync(requestModel);

        return Ok(announcementEntity);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAnnouncementByIdAsync(int id)
    {
        var announcementEntityById = await _announcementService.ReadByIdAsync(id);

        return Ok(announcementEntityById);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnnouncementAsync(int id,
        UpdateAnnouncementRequestModel updateAnnouncementRequestModel)
    {
        var validationResponse = _validator.GetValidationResult(updateAnnouncementRequestModel.Property.Adress);
        if (validationResponse != null)
            return validationResponse;

        var announcementEntityByIdForUpdate =
            await _announcementService.UpdateAsync(id, updateAnnouncementRequestModel);

        return Ok(announcementEntityByIdForUpdate);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnnouncementById(int id)
    {
        await _announcementService.DeleteAsync(id);


        return NoContent();
    }
}