namespace MediCore.DTOs.PrescriptionsDTOs;
public class AddPrescriptionItemResponseDTO
{
    public int Id { get; set; } 
    public int PrescriptionId { get; set; }
    public int MedicationId { get; set; } 
    public string DosageInstructions { get; set; } 
    public int DurationDays { get; set; }
}
