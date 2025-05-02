using MediCore.Enums;
namespace MediCore.DTOs.AppointmentsDTOs;
public class GetPatientsAppointmentsDTO
{
    public int Id { get; set; }
    public DateOnly Date { get; set; }
    public TimeSpan Time { get; set; }
    public TimeSpan Duration { get; set; }
    public APPOINTMENT_STATUS Status { get; set; }
    public APPOINTMENT_TYPE VisitType { get; set; }
    public PatientInfoDTO Patient { get; set; }
}

// THIS CLASS IS FOR SHOW BASIC INFO OF PATIENT
public class PatientInfoDTO
{
    public int Id { get; set; }
    public string PersonalNumber { get; set; }
    public string PhoneNumber { get; set; }
}
