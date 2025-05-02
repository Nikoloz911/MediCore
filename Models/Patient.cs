using MediCore.Enums;
namespace MediCore.Models;
public class Patient
{
    public int Id { get; set; } // Primary Key
    public int UserId { get; set; } // Foreign key to User
    public string PersonalNumber { get; set; } 
    public DateOnly DateOfBirth { get; set; }
    public GENDER GENDER { get; set; } // Enum For Gender
    public string PhoneNumber { get; set; }      
    public string InsuranceDetails { get; set; }
    public string Allergies { get; set; }
    public BLOOD_TYPE BloodType { get; set; } // Enum for blood type
    public User User { get; set; } // Navigation property to User
    public List<Appointment> Appointments { get; set; } // Patient's appointments
    public List<MedicalRecord> MedicalRecords { get; set; } // Patient's medical records

}
