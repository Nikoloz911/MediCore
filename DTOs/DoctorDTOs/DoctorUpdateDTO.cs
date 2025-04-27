namespace MediCore.DTOs.DoctorDTOs;
public class DoctorUpdateDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string WorkingHours { get; set; }
    public int ExperienceYears { get; set; }
}
