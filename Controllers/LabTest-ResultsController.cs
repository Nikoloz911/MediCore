using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
using MediCore.Core;
using MediCore.DTOs.LabTests_ResultsDTOs;
using Microsoft.AspNetCore.Authorization;
namespace MediCore.Controllers;

[Route("api/")]
[ApiController]
public class LabTest_ResultsController : ControllerBase
{
    private readonly ILabTests_Results _labTests_ResultsService;
    public LabTest_ResultsController(ILabTests_Results labTests_ResultsService)
    {
        _labTests_ResultsService = labTests_ResultsService;
    }
    // GET LAB TESTS
    [HttpGet("/api/lab-tests")]
    public IActionResult GetLabTests()
    {
        var response = _labTests_ResultsService.GetAllLabTests();
        if (response.Status == 200)     // OK
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
    // ADD LAB TEST
    [HttpPost("/api/lab-tests")]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult AddLabTest([FromBody] AddLabTestsDTO dto)
    {
        var response = _labTests_ResultsService.AddLabTest(dto);
        if (response.Status == 200)         // OK
        {
            return Ok(response);
        }
        else if (response.Status == 400)    // NOT FOUND
        {
            return BadRequest(response);
        }
        else
        {
            return null;
        }
    }
    // ADD LAB RESULT
    [HttpPost("/api/lab-results")]
    [Authorize(Policy = "AdminOrNurse")]
    public IActionResult AddLabResult([FromBody] AddLabResultDTO dto)
    {    
        var response = _labTests_ResultsService.AddLabResult(dto);

        if (response.Status == 200)     // OK
        {
            return Ok(response); 
        }
        else if (response.Status == 400)        // NOT FOUND
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
    // GET PATIENT LAB RESULTS
    [HttpGet("patient/{patientId}")]
    public ActionResult<ApiResponse<List<GetPatientLabResults>>> GetPatientLabResults(int patientId)
    {
        var response = _labTests_ResultsService.GetPatientLabResults(patientId);

        if (response.Status == 200)             // OK
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
}
