namespace MediCore.Models;
public class Doctor
{
    public int Id { get; set; } // Primary Key
    public int UserId { get; set; } // Foreign key to User
    public int DepartmentId { get; set; } // Foreign key to Department
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string WorkingHours { get; set; }
    public int ExperienceYears { get; set; }
    public User User { get; set; } // Navigation property to User
    public Department Department { get; set; } // Navigation property to Department
    public List<Appointment> Appointments { get; set; } // Doctor's appointments
    public List<MedicalRecord> MedicalRecords { get; set; } // Doctor's medical records
}