using MediCore.Enums;
namespace MediCore.Models;
public class Department
{
    public int Id { get; set; } // Primary Key
    public List<Doctor> Doctors { get; set; } // Navigation property to Doctor
    public string? DepartmentType { get; set; } 
    public List<Appointment> Appointments { get; set; } // department's appointments
}
