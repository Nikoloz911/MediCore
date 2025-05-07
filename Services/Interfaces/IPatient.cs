using MediCore.Core;
using MediCore.DTOs.PatientDTOs;
using MediCore.Models;

namespace MediCore.Services.Interfaces;

public interface IPatient
{
    ApiResponse<List<PatientGetDTO>> GetAllPatients();
    ApiResponse<PatientGetByIdDTO> GetPatientById(int id);
    ApiResponse<PatientCreatedDTO> AddPatient(PatientAddDTO dto);
    ApiResponse<AddPatientUserResponseDTO> AddPatientByUserId(AddPatientUserDTO dto);
    ApiResponse<PatientHistoryDTO> GetPatientHistory(int patientId);
}