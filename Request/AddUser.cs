using MediCore.Enums;
using MediCore.Models;

namespace MediCore.Request;
public class AddUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }  // Admin, Doctor, Nurse, Patient
    public USER_STATUS Status { get; set; } // active, inactive
    // public Doctor? Doctor { get; set; } // Navigation property to Doctor
    // public Patient? Patient { get; set; } // Navigation property to Patient
}
