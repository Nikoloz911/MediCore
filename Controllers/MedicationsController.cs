using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediCore.Services.Interfaces;
namespace MediCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicationsController : ControllerBase
{
    private readonly IMedications _medicationsService;
    public MedicationsController(IMedications medicationsService)
    {
        _medicationsService = medicationsService;
    }
}
