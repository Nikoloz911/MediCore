using MediCore.Enums;
using MediCore.Models;

namespace MediCore.Request;
public class AddDepartments
{
    public DEPARTMENT_TYPE DepartmentType { get; set; } // Enum for department type
    // public List<Appointment> Appointments { get; set; } // department's appointments
}
