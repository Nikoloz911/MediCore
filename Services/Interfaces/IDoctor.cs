using MediCore.Core;
using MediCore.DTOs.DoctorDTOs;

namespace MediCore.Services.Interfaces;
public interface IDoctor
{
    ApiResponse<List<DoctorAllDTO>> GetAllDoctors();
    ApiResponse<DoctorByIdDTO> GetDoctorById(int id);
    ApiResponse<List<DoctorsByDepartmentDTO>> GetDoctorsByDepartment(int departmentId);
    ApiResponse<DoctorScheduleDTO> GetDoctorSchedule(int doctorId);
    ApiResponse<DoctorByIdDTO> UpdateDoctor(int id, DoctorUpdateDTO doctorUpdateDTO);
}
