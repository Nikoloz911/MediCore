namespace MediCore.Models;
public class PrescriptionItem
{
    public int Id { get; set; } 
    public int PrescriptionId { get; set; }
    public int MedicationId { get; set; } 
    public string DosageInstructions { get; set; } 
    public int DurationDays { get; set; }
    public Prescription Prescription { get; set; } 
    public Medication Medication { get; set; } 
}
