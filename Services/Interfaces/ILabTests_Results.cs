using MediCore.Core;
using MediCore.DTOs.LabTests_ResultsDTOs;

namespace MediCore.Services.Interfaces;
public interface ILabTests_Results
{
    ApiResponse<List<GetLabTestsDTO>> GetAllLabTests();
    ApiResponse<AddLabTestsResponseDTO> AddLabTest(AddLabTestsDTO dto);
    ApiResponse<AddLabResultsResponseDTO> AddLabResult(AddLabResultDTO dto);
    ApiResponse<List<GetPatientLabResults>> GetPatientLabResults(int patientId);
}
