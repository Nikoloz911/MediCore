using MediCore.Core;
using MediCore.DTOs.MedicalRecordsDTOs;

namespace MediCore.Services.Interfaces;
public interface IMedicalRecords
{
    ApiResponse<List<GetPatientsMedicalRecordsDTO>> GetMedicalRecordsByPatientId(int patientId);
    ApiResponse<GetMedicalRecordsDTO> GetMedicalRecordById(int id);
    ApiResponse<MedicalRecordResponseDTO> CreateMedicalRecord(CreateMedicalRecordDTO dto);
    ApiResponse<MedicalRecordResponseDTO> UpdateMedicalRecord(int id, UpdateMedicalRecordDTO dto);  
}
