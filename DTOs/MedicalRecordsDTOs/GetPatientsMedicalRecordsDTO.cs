namespace MediCore.DTOs.MedicalRecordsDTOs;
public class GetPatientsMedicalRecordsDTO
{
    public int Id { get; set; } 
    public DateTime CreatedAt { get; set; }
    public int PatientId { get; set; } 
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
}