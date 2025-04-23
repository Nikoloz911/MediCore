namespace MediCore.Models;
public class Prescription
{
    public int Id { get; set; } // Primary Key
    public int MedicalRecordId { get; set; } // Foreign Key to MedicalRecord
    public DateTime IssueDate { get; set; } 
    public DateTime ExpiryDate { get; set; } 
    public string Status { get; set; } // Status (e.g., Pending, Completed, Cancelled)
    public MedicalRecord MedicalRecord { get; set; } // Navigation Property
    public List<PrescriptionItem> PrescriptionItems { get; set; } // prescription's items
}
