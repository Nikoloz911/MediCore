namespace MediCore.Models;
public class Medication
{
    public int Id { get; set; } // Primary Key
    public string Name { get; set; } // Medication name
    public string ActiveSubstance { get; set; } // Active ingredient/substance
    public string Category { get; set; } // Category (e.g., Antibiotic, Analgesic)
    public string Dosage { get; set; } // Dosage instructions (e.g., 500mg, 1 tablet)
    public string Form { get; set; } // Form (e.g., Tablet, Syrup, Injection)
}
