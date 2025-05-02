using MediCore.Enums;
namespace MediCore.DTOs.AppointmentsDTOs;
public class GetAppointmentsDTO
{
    public int Id { get; set; } 
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public TimeSpan Duration { get; set; }
    public APPOINTMENT_STATUS Status { get; set; } 
    public APPOINTMENT_TYPE VisitType { get; set; }
}
