using MediCore.Core;
using MediCore.DTOs.DiagnosesDTOs;

namespace MediCore.Services.Interfaces;
public interface IDiagnoses
{
    ApiResponse<List<GetPatientDiagnosesDTO>> GetDiagnosesByPatientId(int patientId);
    ApiResponse<List<GetMedicalRecordsDiagnosesDTO>> GetDiagnosesByMedicalRecordId(int recordId);
    ApiResponse<AddDiagnosesResponseDTO> AddDiagnosis(AddDiagnosesDTO newDiagnosis);

}
