using MediCore.Enums;
using MediCore.Models;
namespace MediCore.DTOs.MedicalRecordsDTOs;
public class GetPatientsMedicalRecordsDTO
{
    public int Id { get; set; } 
    public DateTime CreatedAt { get; set; }
    public int PatientId { get; set; } 
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
    public PatientBasicInfo Patient { get; set; }
}
public class PatientBasicInfo
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string PersonalNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public GENDER GENDER { get; set; }
    public string PhoneNumber { get; set; }
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public BLOOD_TYPE BloodType { get; set; }

}
