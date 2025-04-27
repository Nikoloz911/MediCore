namespace MediCore.DTOs.DoctorDTOs;
public class DoctorByIdDTO
{
    public int Id { get; set; } // Primary Key
    public int UserId { get; set; } // Foreign key to User
    public string FirstName { get; set; } // User's First Name
    public string LastName { get; set; } // User's Last Name
    public string Email { get; set; } // User's Email
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string WorkingHours { get; set; }
    public int ExperienceYears { get; set; }
    //public User User { get; set; } // Navigation property to User
    //public List<Appointment> Appointments { get; set; } // Doctor's appointments
    //public List<MedicalRecord> MedicalRecords { get; set; } // Doctor's medical records
}
