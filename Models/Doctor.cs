namespace MediCore.Models;
public class Doctor
{
    public int Id { get; set; } 
    public int UserId { get; set; } 
    public int DepartmentId { get; set; } 
    public string Specialty { get; set; }
    public string LicenseNumber { get; set; }
    public string WorkingHours { get; set; }
    public int ExperienceYears { get; set; }
    public string ImageURL { get; set; }
    public User User { get; set; }
    public ChatMessage ChatMessage { get; set; }
    public Department Department { get; set; } 
    public List<Appointment> Appointments { get; set; }
    public List<MedicalRecord> MedicalRecords { get; set; }
}