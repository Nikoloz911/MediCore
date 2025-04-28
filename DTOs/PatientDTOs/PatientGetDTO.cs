using MediCore.Enums;

namespace MediCore.DTOs.PatientDTOs;
public class PatientGetDTO
{
    public int Id { get; set; } // Primary Key
    public int UserId { get; set; } // Foreign key to User
    public string PersonalNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public GENDER GENDER { get; set; } // Enum For Gender
    public string ContactInfo { get; set; }
}
