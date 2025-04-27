using AutoMapper;
using MediCore.Data;
using Microsoft.AspNetCore.Mvc;
using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;
using MediCore.Services.Interfaces;
using MediCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MediCore.Services.Implementations
{
    public class DoctorService : IDoctor
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DoctorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all doctors (Basic Info)
        public ApiResponse<List<DoctorAllDTO>> GetAllDoctors()
        {
            var doctors = _context.Doctors.Include(d => d.User).ToList(); 
            if (!doctors.Any())
            {
                return new ApiResponse<List<DoctorAllDTO>>
                {
                    Status = 404,
                    Message = "No doctors found.",
                    Data = null
                };
            }
            var doctorDtos = _mapper.Map<List<DoctorAllDTO>>(doctors);
            return new ApiResponse<List<DoctorAllDTO>>
            {
                Status = 200,
                Message = "Doctors retrieved successfully.",
                Data = doctorDtos
            };
        }
        // Get doctor by ID
        public ApiResponse<DoctorByIdDTO> GetDoctorById(int id)
        {
            var doctor = _context.Doctors
                .Include(d => d.User)
                .FirstOrDefault(d => d.UserId == id);
            if (doctor == null)
            {
                return new ApiResponse<DoctorByIdDTO>
                {
                    Status = 404,
                    Message = "Doctor not found.",
                    Data = null
                };
            }
            var doctorDto = _mapper.Map<DoctorByIdDTO>(doctor);
            return new ApiResponse<DoctorByIdDTO>
            {
                Status = 200,
                Message = "Doctor retrieved successfully.",
                Data = doctorDto
            };
        }
    }
}
