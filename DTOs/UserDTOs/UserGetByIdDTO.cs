using MediCore.Enums;
namespace MediCore.DTOs.UserDTOs;
public class UserGetByIdDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    // public string Password { get; set; }
    public USER_ROLE Role { get; set; }  // Admin, Doctor, Nurse, Patient
    public USER_STATUS Status { get; set; } // active, inactive
}
