using MediCore.Core;
using MediCore.DTOs.MedicationsDTOs;

namespace MediCore.Services.Interfaces;
public interface IMedications
{
    ApiResponse<List<GetAllMedications>> GetMedications(
        string? name = null,
        string? activeSubstance = null,
        string? category = null,
        string? form = null
    );
    ApiResponse<GetMedication> GetMedicationById(int id);
    ApiResponse<AddMedicationResponseDTO> AddMedication(AddMedicationDTO dto);
    ApiResponse<AddMedicationResponseDTO> UpdateMedication(int id, UpdateMedicationDTO dto);

}
