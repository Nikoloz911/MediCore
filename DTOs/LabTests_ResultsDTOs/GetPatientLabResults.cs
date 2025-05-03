using MediCore.Enums;
namespace MediCore.DTOs.LabTests_ResultsDTOs;
public class GetPatientLabResults
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int LabTestId { get; set; }
    public string Result { get; set; }
    public DateOnly TestDate { get; set; }
    public string PerformingLab { get; set; }
    public PatientLabResultsBasicInfo Patient { get; set; }
}
public class PatientLabResultsBasicInfo
{
    public int Id { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public GENDER GENDER { get; set; } 
    public string PhoneNumber { get; set; }
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public BLOOD_TYPE BloodType { get; set; } 
}
