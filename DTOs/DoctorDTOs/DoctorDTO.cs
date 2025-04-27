using MediCore.Models;

namespace MediCore.DTOs.DoctorDTOs;
public class DoctorDTO
{
    public int Id { get; set; } // Primary Key
    public int UserId { get; set; } // Foreign key to User
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string WorkingHours { get; set; }
    public int ExperienceYears { get; set; }
    //public User User { get; set; } // Navigation property to User
    //public List<Appointment> Appointments { get; set; } // Doctor's appointments
    //public List<MedicalRecord> MedicalRecords { get; set; } // Doctor's medical records
}
