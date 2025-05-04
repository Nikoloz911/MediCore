using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.Models;
using MediCore.Core;
using MediCore.DTOs.PrescriptionsDTOs;
using Microsoft.AspNetCore.Authorization;
namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptions _prescriptionsService;
    public PrescriptionsController(IPrescriptions prescriptionsService)
    {
        _prescriptionsService = prescriptionsService;
    }
    // GET PRESCTIPTIONS BY PATIENT ID
    [HttpGet("prescriptions/patient/{patientId}")]
    public ActionResult<ApiResponse<List<GetPatientPrescriptionsDTO>>> GetPatientPrescriptions(int patientId)
    {
        var response = _prescriptionsService.GetPrescriptionsByPatientId(patientId);

       if(response.Status == 200)           // OK
        {
            return Ok(response);

        }
       else if (response.Status == 404)         // NOT FOUND
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
    // GET PRESCTIPTIONS BY ID
    [HttpGet("prescriptions/{id}")]
    public ActionResult<ApiResponse<GetPrescriptionsByIdDTO>> GetPrescriptionById(int id)
    {
        var response = _prescriptionsService.GetPrescriptionById(id);

        if (response.Status == 200)         // OK
        {
            return Ok(response);
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
    // ADD NEW PRESCRIPTION
    [HttpPost("prescriptions")]
    [Authorize(Policy = "DoctorOnly")]
    public ActionResult<ApiResponse<AddPrescriptionsResponseDTO>> AddPrescription([FromBody] AddPrescriptionsDTO dto)
    {
        var response = _prescriptionsService.AddPrescription(dto);

        if (response.Status == 200)     // OK
        {
            return Ok(response);
        }
        else if (response.Status == 400)        // BAD REQUEST
        {
            return BadRequest(response);
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
    // UPDATE PRESCRIPTION
    [HttpPut("prescriptions/{id}")]
    [Authorize(Policy = "DoctorOnly")]
    public ActionResult<ApiResponse<UpdatePrescriptionResponseDTO>> UpdatePrescription(int id, [FromBody] UpdatePrescriptionDTO dto)
    {
        var response = _prescriptionsService.UpdatePrescription(id, dto);

        if (response.Status == 200)     // OK
        {
            return Ok(response);
        }
        else if (response.Status == 400)        // BAD REQUEST
        {
            return BadRequest(response);
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
    // GET ACTIVE PRESCRIPTIONS BY PATIENT ID
    [HttpGet("active/patient/{patientId}")]
    public ActionResult<ApiResponse<List<GetActivePrescriptionsDTO>>> GetActivePrescriptionsByPatientId(int patientId)
    {
        var response = _prescriptionsService.GetActivePrescriptionsByPatientId(patientId);

        if (response.Status == 200)         // OK
        {
            return Ok(response);
        }
        else if (response.Status == 404)        // BAD REQUEST
        {
            return NotFound(response);
        }
        else
        {
            return null;
        }
    }
    /// PRESCRIPTION ITEMS  /// PRESCRIPTION ITEMS  /// PRESCRIPTION ITEMS  /// PRESCRIPTION ITEMS
    [HttpPost("prescriptions/item")]
    public ActionResult<ApiResponse<AddPrescriptionItemResponseDTO>> AddPrescriptionItem([FromBody] AddPrescriptionItemDTO dto)
    {
        var response = _prescriptionsService.AddPrescriptionItem(dto);

        if (response.Status == 200)         // OK
        {
            return Ok(response);
        }
        else if (response.Status == 400)        // BAD REQUEST
        {
            return BadRequest(response);
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
    /// PRESCRIPTION ITEMS  /// PRESCRIPTION ITEMS  /// PRESCRIPTION ITEMS  /// PRESCRIPTION ITEMS
}