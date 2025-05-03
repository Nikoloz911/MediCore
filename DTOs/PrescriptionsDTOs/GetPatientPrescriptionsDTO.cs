using MediCore.Enums;
using MediCore.Models;

namespace MediCore.DTOs.PrescriptionsDTOs;
public class GetPatientPrescriptionsDTO
{
    public int Id { get; set; }
    public int MedicalRecordId { get; set; } 
    public DateOnly IssueDate { get; set; }
    public DateOnly ExpiryDate { get; set; }
    public PRESCRIPTION_STATUS Status { get; set; }
    public PrescriptinosPatientBasicInfo Patient { get; set; }
}
public class PrescriptinosPatientBasicInfo
{
    public int UserId { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public GENDER GENDER { get; set; } 
    public string PhoneNumber { get; set; }
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public BLOOD_TYPE BloodType { get; set; } 
}