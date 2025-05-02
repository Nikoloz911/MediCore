namespace MediCore.DTOs.PatientDTOs;

public class PatientAddDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PersonalNumber { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public string BloodType { get; set; }
}
