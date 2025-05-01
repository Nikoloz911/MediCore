using AutoMapper;
using FluentValidation;
using MediCore.Services.Interfaces;
using MediCore.Data;
using MediCore.Request;

namespace MediCore.Services.Implementations;
public class MedicalRecordsService : IMedicalRecords
{
    private readonly DataContext _context;
    //private readonly IValidator<> _userValidator;
    private readonly IMapper _mapper;

    public MedicalRecordsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
}
