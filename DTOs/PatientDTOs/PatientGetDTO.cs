using MediCore.Enums;
namespace MediCore.DTOs.PatientDTOs;
public class PatientGetDTO
{
    public int Id { get; set; } 
    public int UserId { get; set; } 
    public string PersonalNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public GENDER GENDER { get; set; } 
    public string ContactInfo { get; set; }
}
