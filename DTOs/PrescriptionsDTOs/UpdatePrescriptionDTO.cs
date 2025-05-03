namespace MediCore.DTOs.PrescriptionsDTOs;
public class UpdatePrescriptionDTO
{
    public int MedicalRecordId { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public string Status { get; set; }
}
