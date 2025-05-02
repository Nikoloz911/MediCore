using MediCore.Enums;

namespace MediCore.DTOs.DiagnosesDTOs;
public class GetPatientDiagnosesDTO
{
    public int Id { get; set; } 
    public int MedicalRecordId { get; set; } 
    public string ICD10Code { get; set; }
    public string Description { get; set; }
    public string AdditionalComments { get; set; }
    public DiagnosesPatientBasicInfo Patient { get; set; } 
}
public class DiagnosesPatientBasicInfo
{
    public int UserId { get; set; } 
    public DateOnly DateOfBirth { get; set; }
    public GENDER GENDER { get; set; } 
    public string PhoneNumber { get; set; }
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public BLOOD_TYPE BloodType { get; set; }
}
