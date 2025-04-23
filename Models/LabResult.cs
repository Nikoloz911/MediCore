namespace MediCore.Models;
public class LabResult
{
    public int Id { get; set; } // Primary Key
    public int PatientId { get; set; } // Foreign Key to Patient
    public int LabTestId { get; set; } // Foreign Key to LabTest
    public string Result { get; set; } // Result of the lab test (e.g., "Positive", "120 mg/dL")
    public DateTime TestDate { get; set; } // Date the test was performed
    public string PerformingLab { get; set; } // Lab that performed the test
    public Patient Patient { get; set; } // Navigation property to Patient
    public LabTest LabTest { get; set; } // Navigation property to LabTest
}
