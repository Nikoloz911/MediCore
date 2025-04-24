namespace MediCore.Models;
public class PrescriptionItem
{
    public int Id { get; set; } // Primary Key
    public int PrescriptionId { get; set; } // Foreign Key to Prescription
    public int MedicationId { get; set; } // Foreign Key to Medication
    public string DosageInstructions { get; set; } // Dosage instructions (e.g., 1 tablet daily)
    public int DurationDays { get; set; }
    public Prescription Prescription { get; set; } // Navigation Property
    public Medication Medication { get; set; } // Navigation Property
}
