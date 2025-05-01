using MediCore.Enums;
using MediCore.Models;
namespace MediCore.DTOs.AppointmentsDTOs;
public class GetAppointmentsByIdDTO
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public TimeSpan Duration { get; set; }
    public APPOINTMENT_STATUS Status { get; set; }
    public APPOINTMENT_TYPE VisitType { get; set; }
    public DoctorBasicDTO Doctor { get; set; }
    public PatientBasicDTO Patient { get; set; }
    public DepartmentBasicDTO Department { get; set; }
}

// THIS CLASS IS FOR SHOW BASIC INFO OF DOCTOR
public class DoctorBasicDTO
{
    public int Id { get; set; }
    public string Specialty { get; set; }
    public string WorkingHours { get; set; }
}
// THIS CLASS IS FOR SHOW BASIC INFO OF PATIENT
public class PatientBasicDTO
{
    public int Id { get; set; }
    public string PersonalNumber { get; set; }
    public string ContactInfo { get; set; }
}
// THIS CLASS IS FOR SHOW BASIC INFO OF DEPARTMENT
public class DepartmentBasicDTO
{
    public int Id { get; set; }
    public DEPARTMENT_TYPE? DepartmentType { get; set; } 
    public string? DepartmentName { get; set; } 
}
