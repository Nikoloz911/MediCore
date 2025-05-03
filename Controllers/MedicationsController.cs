using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.Core;
using MediCore.DTOs.MedicationsDTOs;
using Microsoft.AspNetCore.Authorization;
namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class MedicationsController : ControllerBase
{
    private readonly IMedications _medicationsService;
    public MedicationsController(IMedications medicationsService)
    {
        _medicationsService = medicationsService;
    }
    // GET ALL MEDICATIONS WITH FILTERS
    [HttpGet("medications")]
    public ActionResult<ApiResponse<List<GetAllMedications>>> GetMedications(
    [FromQuery] string? name,
    [FromQuery] string? activeSubstance,
    [FromQuery] string? category,
    [FromQuery] string? form)
    {
        var response = _medicationsService.GetMedications(name, activeSubstance, category, form);

        if (response.Status == 200)
        {
            return Ok(response);             // OK

        }
        else if (response.Status == 404)
        {
            return NotFound(response);          // 404 Not Found
        }
        else
        {
            return null;
        }
    }
    // GET MEDICATION BY ID
    [HttpGet("medications/{id}")]
    public ActionResult<ApiResponse<GetMedication>> GetMedicationById(int id)
    {
        var response = _medicationsService.GetMedicationById(id);

        if (response.Status == 200)
        {
            return Ok(response);                // OK
        }
        else if (response.Status == 404)        // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
    // ADD MEDICATION
    [HttpPost("medications")]
    [Authorize(Policy = "AdminOrDoctor")]
    public ActionResult<ApiResponse<AddMedicationResponseDTO>> AddMedication([FromBody] AddMedicationDTO dto)
    {
        var response = _medicationsService.AddMedication(dto);

        if(response.Status == 200)     //  OK
        {
            return Ok(response);           
        }
        else if (response.Status == 400)        // BAD REQUEST
        {
            return BadRequest(response);
        }
        else
        {
            return null;
        }
    }
    // UPDATE MEDICATION
    [HttpPut("medications/{id}")]
    [Authorize(Policy = "AdminOrDoctor")]
    public ActionResult<ApiResponse<AddMedicationResponseDTO>> UpdateMedication(int id, [FromBody] UpdateMedicationDTO dto)
    {
        var response = _medicationsService.UpdateMedication(id, dto);
        if(response.Status == 200)     // OK
        {
            return Ok(response);
        }
        else if (response.Status == 404)        // NOT FOUND
        {
            return NotFound(response);
        }
        else if (response.Status == 400)        // BAD REQUEST
        {
            return BadRequest(response);
        }
        else
        {
            return null;
        }
    }
}
