using AutoMapper;
using FluentValidation;
using MediCore.Data;
using MediCore.Services.Interfaces;

namespace MediCore.Services.Implementations;
public class MedicationsService : IMedications
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    //private readonly IValidator<Medication> _validator;
    public MedicationsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
