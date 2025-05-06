using MediCore.Enums;
namespace MediCore.Models;
public class Appointment
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }           
    public TimeSpan Time { get; set; }           
    public TimeSpan Duration { get; set; }
    public int DoctorId { get; set; }  
    public int PatientId { get; set; } 
    public int DepartmentId { get; set; } 
    public Doctor Doctor { get; set; } 
    public Patient Patient { get; set; } 
    public Department Department { get; set; } 
    public APPOINTMENT_STATUS Status { get; set; }   
    public APPOINTMENT_TYPE VisitType { get; set; } 

}
