using MediCore.Enums;
namespace MediCore.DTOs.AppointmentsDTOs;
public class AddAppointmentResponseDTO
{
    public int Id { get; set; } 
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public TimeSpan Duration { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public int DepartmentId { get; set; }
    public APPOINTMENT_STATUS Status { get; set; } 
    public APPOINTMENT_TYPE VisitType { get; set; }
}
