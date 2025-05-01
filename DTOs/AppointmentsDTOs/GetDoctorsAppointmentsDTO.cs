using MediCore.Enums;
namespace MediCore.DTOs.AppointmentsDTOs;
public class GetDoctorsAppointmentsDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public TimeSpan Duration { get; set; }
    public APPOINTMENT_STATUS Status { get; set; }
    public APPOINTMENT_TYPE VisitType { get; set; }
    public DoctorInfoDTO Doctor { get; set; }
}

// THIS CLASS IS FOR SHOW BASIC INFO OF DOCTOR
public class DoctorInfoDTO
{
    public int Id { get; set; }
    public string Specialty { get; set; }
    public string WorkingHours { get; set; }
    public int ExperienceYears { get; set; }
}