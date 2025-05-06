namespace MediCore.Models;
public class LabResult
{
    public int Id { get; set; } 
    public int PatientId { get; set; } 
    public int LabTestId { get; set; }
    public string Result { get; set; } 
    public DateOnly TestDate { get; set; } 
    public string PerformingLab { get; set; }
    public Patient Patient { get; set; }
    public LabTest LabTest { get; set; } 
}
