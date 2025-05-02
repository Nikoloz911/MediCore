namespace MediCore.DTOs.MedicalRecordsDTOs;
public class UpdateMedicalRecordDTO
{
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
    public string Measurements { get; set; }
    public int DoctorId { get; set; }
}
