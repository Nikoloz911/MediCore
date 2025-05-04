using MediCore.Core;
using MediCore.DTOs.PrescriptionsDTOs;

namespace MediCore.Services.Interfaces;
public interface IPrescriptions
{
    ApiResponse<List<GetPatientPrescriptionsDTO>> GetPrescriptionsByPatientId(int patientId);
    ApiResponse<GetPrescriptionsByIdDTO> GetPrescriptionById(int id);
    ApiResponse<AddPrescriptionsResponseDTO> AddPrescription(AddPrescriptionsDTO dto);
    ApiResponse<UpdatePrescriptionResponseDTO> UpdatePrescription(int id, UpdatePrescriptionDTO dto);
    ApiResponse<List<GetActivePrescriptionsDTO>> GetActivePrescriptionsByPatientId(int patientId);
    ApiResponse<AddPrescriptionItemResponseDTO> AddPrescriptionItem(AddPrescriptionItemDTO dto);
}
