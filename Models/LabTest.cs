namespace MediCore.Models;
public class LabTest
{
    public int Id { get; set; } // Primary Key
    public string TestType { get; set; } // Type of the test (e.g., Blood Test, Urine Test)
    public string NormalRange { get; set; } // Normal range for the test result
    public string Unit { get; set; } // Unit of measurement (e.g., mg/dL, g/L)
}
