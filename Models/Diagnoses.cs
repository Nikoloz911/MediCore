namespace MediCore.Models;
public class Diagnoses
{
    public int Id { get; set; } // Primary Key
    public int MedicalRecordId { get; set; } // Foreign Key
    public string ICD10Code { get; set; }
    public string Description { get; set; }
    public string AdditionalComments { get; set; }
    public MedicalRecord MedicalRecord { get; set; } // Navigation Property
}
