using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
namespace MediCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiagnosesController : ControllerBase
{
    private readonly IDiagnoses _diagnosesService;

    public DiagnosesController(IDiagnoses diagnosesService)
    {
        _diagnosesService = diagnosesService;
    }
}
