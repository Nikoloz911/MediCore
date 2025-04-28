using MediCore.Enums;
namespace MediCore.Models;
public class Department
{
    public int Id { get; set; } // Primary Key
    public List<Doctor> Doctors { get; set; } // Navigation property to Doctor
    public DEPARTMENT_TYPE DepartmentType { get; set; } // Enum for department type
    public List<Appointment> Appointments { get; set; } // department's appointments
}
