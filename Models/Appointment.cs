using MediCore.Enums;
namespace MediCore.Models;
public class Appointment
{
    public int Id { get; set; } // Primary Key
    public DateTime Date { get; set; }           
    public TimeSpan Time { get; set; }           
    public TimeSpan Duration { get; set; }
    public int DoctorId { get; set; }   // Foreign Key to Doctor
    public int PatientId { get; set; }  // Foreign Key to Patient
    public int DepartmentId { get; set; }  // Foreign Key to Department
    public Doctor Doctor { get; set; } // Navigation property to Doctor
    public Patient Patient { get; set; } // Navigation property to Patient
    public Department Department { get; set; } // Navigation property to Department
    public APPOINTMENT_STATUS Status { get; set; }  // Enum for appointment status    
    public APPOINTMENT_TYPE VisitType { get; set; }  // Enum for appointment type   

}
