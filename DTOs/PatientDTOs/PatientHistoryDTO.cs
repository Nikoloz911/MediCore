using MediCore.Models;

namespace MediCore.DTOs.PatientDTOs;
public class PatientHistoryDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PersonalNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public string BloodType { get; set; }
    public List<MedicalRecordDTO> MedicalRecords { get; set; }
}

public class MedicalRecordDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
    public string Measurements { get; set; }
}

