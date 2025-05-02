namespace MediCore.DTOs.MedicalRecordsDTOs;
public class CreateMedicalRecordDTO
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
    public string Measurements { get; set; }
}
