using MediCore.Enums;

namespace MediCore.DTOs.UserDTOs;
public class AddUserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }   // Admin, Doctor, Nurse, Patient
}
