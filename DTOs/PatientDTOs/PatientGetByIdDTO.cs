using MediCore.Enums;
using MediCore.Models;
namespace MediCore.DTOs.PatientDTOs;
public class PatientGetByIdDTO
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
    public List<Appointment> Appointments { get; set; }
    public List<MedicalRecord> MedicalRecords { get; set; } 
}
