using MediCore.Enums;
namespace MediCore.Models;
public class User
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public USER_ROLE Role { get; set; }  // Admin, Doctor, Nurse, Patient
    public USER_STATUS Status { get; set; } // active, inactive
    public Doctor? Doctor { get; set; } // Navigation property to Doctor
    public Patient? Patient { get; set; } // Navigation property to Patient
}
