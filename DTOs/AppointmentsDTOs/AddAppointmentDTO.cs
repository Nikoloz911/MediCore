namespace MediCore.DTOs.AppointmentsDTOs;
public class AddAppointmentDTO
{
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public TimeSpan Duration { get; set; }
    public int DoctorId { get; set; }   
    public int PatientId { get; set; }  
    public int DepartmentId { get; set; } 
    public string Status { get; set; }    
    public string VisitType { get; set; }  
}
