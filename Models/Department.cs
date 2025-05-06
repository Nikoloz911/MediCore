using MediCore.Enums;
namespace MediCore.Models;
public class Department
{
    public int Id { get; set; }
    public List<Doctor> Doctors { get; set; }
    public string? DepartmentType { get; set; } 
    public List<Appointment> Appointments { get; set; }
}
