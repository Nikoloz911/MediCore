namespace MediCore.Models;
public class Doctor
{
    public int Id { get; set; }
    public int UserId { get; set; } // Foreign key to User
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string WorkingHours { get; set; } 
    public int ExperienceYears { get; set; }
    public User User { get; set; } // Navigation property to User
}
