namespace MediCore.DTOs.DiagnosesDTOs;
public class GetMedicalRecordsDiagnosesDTO
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; }
    public string ICD10Code { get; set; }
    public string Description { get; set; }
    public string AdditionalComments { get; set; }
    public MedicalRecordBasicInfo MedicalRecord { get; set; }
}
public class MedicalRecordBasicInfo
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; } 
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
    public string Measurements { get; set; }
}
