namespace MediCore.DTOs.LabTests_ResultsDTOs;
public class AddLabResultDTO
{
    public int PatientId { get; set; } 
    public int LabTestId { get; set; } 
    public string Result { get; set; } 
    public DateOnly TestDate { get; set; } 
    public string PerformingLab { get; set; }
}
