﻿namespace MediCore.DTOs.UserDTOs;
public class PublicUserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string VerificationCode { get; set; }
    public string Status { get; set; }
    public string Role { get; set; }
}
