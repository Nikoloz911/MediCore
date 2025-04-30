namespace MediCore.DTOs.DoctorDTOs;
public class DoctorByIdDTO
{
    public int Id { get; set; } 
    public int UserId { get; set; } 
    public string FirstName { get; set; } 
    public string LastName { get; set; } 
    public string Email { get; set; } 
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string WorkingHours { get; set; }
    public int ExperienceYears { get; set; }
    public string DepartmentType { get; set; }
}
