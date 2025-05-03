using MediCore.Services.Interfaces; 
using MediCore.Data;
using AutoMapper;
namespace MediCore.Services.Implementations;
public class ReportsService : IReports
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public ReportsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
