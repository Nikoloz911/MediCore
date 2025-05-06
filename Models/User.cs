using MediCore.Enums;
namespace MediCore.Models;
public class User
{
    public int Id { get; set; } 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? VerificationCode { get; set; }
    public DateTime? VerificationCodeExpiry { get; set; }
    public USER_ROLE Role { get; set; }  
    public USER_STATUS Status { get; set; }
    public Doctor? Doctor { get; set; } 
    public Patient? Patient { get; set; }
}
