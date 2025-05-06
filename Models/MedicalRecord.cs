namespace MediCore.Models;
public class MedicalRecord
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; } 
    public string Complaints { get; set; }
    public string Symptoms { get; set; }
    public string Measurements { get; set; }
    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; } 
    public List<Diagnoses> Diagnoses { get; set; }
    public List<Prescription> Prescriptions { get; set; } 
}
