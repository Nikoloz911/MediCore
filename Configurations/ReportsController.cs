using MediCore.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediCore.Configurations;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
    private readonly IReports _reportService;
    public ReportsController(IReports reportService)
    {
        _reportService = reportService;
    }
}
