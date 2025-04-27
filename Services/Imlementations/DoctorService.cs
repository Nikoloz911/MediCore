using AutoMapper;
using MediCore.Data;
using Microsoft.AspNetCore.Mvc;
using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Services.Interaces;
namespace MediCore.Services.Imlementations;
public class DoctorService : IDoctor
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public DoctorService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    // Get all doctors
    public ApiResponse<List<DoctorDTO>> GetAllDoctors()
    {
        var doctors = _context.Doctors.ToList();
        if (!doctors.Any())
        {
            return new ApiResponse<List<DoctorDTO>>
            {
                Status = 404,
                Message = "No doctors found.",
                Data = null
            };
        }
        var doctorDtos = _mapper.Map<List<DoctorDTO>>(doctors);
        return new ApiResponse<List<DoctorDTO>>
        {
            Status = 200,
            Message = "Doctors retrieved successfully.",
            Data = doctorDtos
        };
    }
}