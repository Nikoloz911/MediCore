using AutoMapper;
using FluentValidation;
using MediCore.Data;
using MediCore.Services.Interfaces;
namespace MediCore.Services.Implementations;
public class DiagnosesService : IDiagnoses
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    // private readonly IValidator<> _validator;
    public DiagnosesService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
}
