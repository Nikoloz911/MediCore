using MediCore.Enums;
namespace MediCore.Request;
public class AddUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? VerificationCode { get; set; }
    public DateTime? VerificationCodeExpiry { get; set; }
    public string Role { get; set; } 
    public USER_STATUS Status { get; set; }
}
