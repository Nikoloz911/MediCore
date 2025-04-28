using MediCore.Core;
using MediCore.DTOs.PatientDTOs;

namespace MediCore.Services.Interfaces;
public interface IPatient
{
    ApiResponse<List<PatientGetDTO>> GetAllPatients();
    ApiResponse<PatientGetByIdDTO> GetPatientById(int id);
}
