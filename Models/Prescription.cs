using MediCore.Enums;
namespace MediCore.Models;
public class Prescription
{
    public int Id { get; set; } // Primary Key
    public int MedicalRecordId { get; set; } // Foreign Key to MedicalRecord
    public DateTime IssueDate { get; set; } 
    public DateTime ExpiryDate { get; set; } 
    public PRESCRIPTION_STATUS Status { get; set; } 
    public MedicalRecord MedicalRecord { get; set; } // Navigation Property
    public List<PrescriptionItem> PrescriptionItems { get; set; } // prescription's items
}
