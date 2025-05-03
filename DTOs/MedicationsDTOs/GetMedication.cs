namespace MediCore.DTOs.MedicationsDTOs;
public class GetMedication
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ActiveSubstance { get; set; }
    public string Category { get; set; }
    public string Dosage { get; set; }
    public string Form { get; set; }
}
