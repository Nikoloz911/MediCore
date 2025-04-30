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
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string ContactInfo { get; set; }
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public string BloodType { get; set; }
    public List<MedicalRecord> MedicalRecords { get; set; }
}
