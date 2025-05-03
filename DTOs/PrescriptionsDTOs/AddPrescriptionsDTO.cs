namespace MediCore.DTOs.PrescriptionsDTOs;
public class AddPrescriptionsDTO
{
    public int MedicalRecordId { get; set; } 
    public DateOnly ExpiryDate { get; set; }
    public string Status { get; set; }
}
