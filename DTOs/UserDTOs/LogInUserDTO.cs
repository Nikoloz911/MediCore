namespace MediCore.DTOs.UserDTOs;
public class LogInUserDTO // login should return this data
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string Status { get; set; } // verified, unverified, active, inactive
    public string Token { get; set; }
}
