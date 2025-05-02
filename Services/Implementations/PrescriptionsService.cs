using AutoMapper;
using FluentValidation;
using MediCore.Data;
using MediCore.DTOs.DiagnosesDTOs;
using MediCore.Services.Interfaces;

namespace MediCore.Services.Implementations;
public class PrescriptionsService : IPrescriptions
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    //private readonly IValidator<AddDiagnosesDTO> _validator;
    //private readonly IValidator<UpdateDiagnosesDTO> _updateValidator;
    public PrescriptionsService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

}
