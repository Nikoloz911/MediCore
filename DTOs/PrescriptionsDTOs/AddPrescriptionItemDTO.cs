namespace MediCore.DTOs.PrescriptionsDTOs;
public class AddPrescriptionItemDTO
{
    public int PrescriptionId { get; set; } 
    public int MedicationId { get; set; } 
    public string DosageInstructions { get; set; }
    public int DurationDays { get; set; }
}
