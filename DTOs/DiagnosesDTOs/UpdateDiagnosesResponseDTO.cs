namespace MediCore.DTOs.DiagnosesDTOs;
public class UpdateDiagnosesResponseDTO
{
    public int Id { get; set; } 
    public int MedicalRecordId { get; set; } 
    public string ICD10Code { get; set; }
    public string Description { get; set; }
    public string AdditionalComments { get; set; }
}
