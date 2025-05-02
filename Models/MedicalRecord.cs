namespace MediCore.Models;
public class MedicalRecord
{
    public int Id { get; set; } // Primary Key
    public DateTime CreatedAt { get; set; }
    public int DoctorId { get; set; } // Foreign Key
    public int PatientId { get; set; } // Foreign Key
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
    public string Measurements { get; set; }
    public Doctor Doctor { get; set; } // Navigation Property
    public Patient Patient { get; set; } // Navigation Property
    public List<Diagnoses> Diagnoses { get; set; } // medical record's diagnoses
    public List<Prescription> Prescriptions { get; set; } // medical record's prescriptions
}
