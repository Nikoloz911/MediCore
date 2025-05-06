namespace MediCore.Models;
public class Medication
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ActiveSubstance { get; set; } 
    public string Category { get; set; }
    public string Dosage { get; set; }
    public string Form { get; set; }
    public List<PrescriptionItem> PrescriptionItems { get; set; }
}
