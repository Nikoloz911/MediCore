using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;

namespace MediCore.Services.Interfaces;
public interface IDoctor
{
    ApiResponse<List<DoctorAllDTO>> GetAllDoctors();
    ApiResponse<DoctorByIdDTO> GetDoctorById(int id); 
}
