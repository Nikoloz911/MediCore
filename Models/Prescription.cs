using MediCore.Enums;
namespace MediCore.Models;
public class Prescription
{
    public int Id { get; set; } 
    public int MedicalRecordId { get; set; }
    public DateOnly IssueDate { get; set; } 
    public DateOnly ExpiryDate { get; set; } 
    public PRESCRIPTION_STATUS Status { get; set; } 
    public MedicalRecord MedicalRecord { get; set; } 
    public List<PrescriptionItem> PrescriptionItems { get; set; } 
}
