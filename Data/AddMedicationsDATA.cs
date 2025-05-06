using MediCore.Models;
namespace MediCore.Data
{
    public class AddMedicationsDATA
    {
        public static void InitializeData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            var medications = new List<Medication>
            {
                new Medication
                {
                    Name = "Aspirin",
                    ActiveSubstance = "Acetylsalicylic Acid",
                    Category = "Analgesic",
                    Dosage = "500mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Ibuprofen",
                    ActiveSubstance = "Ibuprofen",
                    Category = "Analgesic",
                    Dosage = "200mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Amoxicillin",
                    ActiveSubstance = "Amoxicillin",
                    Category = "Antibiotic",
                    Dosage = "250mg",
                    Form = "Capsule"
                },
                new Medication
                {
                    Name = "Paracetamol",
                    ActiveSubstance = "Paracetamol",
                    Category = "Analgesic",
                    Dosage = "500mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Metformin",
                    ActiveSubstance = "Metformin",
                    Category = "Antidiabetic",
                    Dosage = "500mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Losartan",
                    ActiveSubstance = "Losartan Potassium",
                    Category = "Antihypertensive",
                    Dosage = "50mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Simvastatin",
                    ActiveSubstance = "Simvastatin",
                    Category = "Statin",
                    Dosage = "10mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Omeprazole",
                    ActiveSubstance = "Omeprazole",
                    Category = "Proton Pump Inhibitor",
                    Dosage = "20mg",
                    Form = "Capsule"
                },
                new Medication
                {
                    Name = "Loratadine",
                    ActiveSubstance = "Loratadine",
                    Category = "Antihistamine",
                    Dosage = "10mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Ciprofloxacin",
                    ActiveSubstance = "Ciprofloxacin",
                    Category = "Antibiotic",
                    Dosage = "500mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Levothyroxine",
                    ActiveSubstance = "Levothyroxine Sodium",
                    Category = "Thyroid Medication",
                    Dosage = "50mcg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Diphenhydramine",
                    ActiveSubstance = "Diphenhydramine",
                    Category = "Antihistamine",
                    Dosage = "25mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Fluoxetine",
                    ActiveSubstance = "Fluoxetine",
                    Category = "Antidepressant",
                    Dosage = "20mg",
                    Form = "Capsule"
                },
                new Medication
                {
                    Name = "Hydrochlorothiazide",
                    ActiveSubstance = "Hydrochlorothiazide",
                    Category = "Diuretic",
                    Dosage = "25mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Prednisone",
                    ActiveSubstance = "Prednisone",
                    Category = "Corticosteroid",
                    Dosage = "10mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Enalapril",
                    ActiveSubstance = "Enalapril Maleate",
                    Category = "Antihypertensive",
                    Dosage = "10mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Furosemide",
                    ActiveSubstance = "Furosemide",
                    Category = "Diuretic",
                    Dosage = "40mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Tamsulosin",
                    ActiveSubstance = "Tamsulosin",
                    Category = "Alpha Blocker",
                    Dosage = "0.4mg",
                    Form = "Capsule"
                },
                new Medication
                {
                    Name = "Fluticasone",
                    ActiveSubstance = "Fluticasone Propionate",
                    Category = "Steroid",
                    Dosage = "50mcg",
                    Form = "Inhaler"
                },
                new Medication
                {
                    Name = "Naproxen",
                    ActiveSubstance = "Naproxen",
                    Category = "NSAID",
                    Dosage = "250mg",
                    Form = "Tablet"
                },
                new Medication
                {
                    Name = "Doxycycline",
                    ActiveSubstance = "Doxycycline Hyclate",
                    Category = "Antibiotic",
                    Dosage = "100mg",
                    Form = "Capsule"
                }
            };

            foreach (var medication in medications)
            {
                var exists = context.Medications.Any(m =>
                    m.Name == medication.Name &&
                    m.ActiveSubstance == medication.ActiveSubstance &&
                    m.Dosage == medication.Dosage
                );
                if (!exists)
                {
                    context.Medications.Add(medication);
                }
            }
            context.SaveChanges();
        }
    }
}
