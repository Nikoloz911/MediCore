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
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/7/8/78697_334.jpg"
                },
                new Medication
                {
                    Name = "Ibuprofen",
                    ActiveSubstance = "Ibuprofen",
                    Category = "Analgesic",
                    Dosage = "200mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/f/i/file_166.jpg"
                },
                new Medication
                {
                    Name = "Amoxicillin",
                    ActiveSubstance = "Amoxicillin",
                    Category = "Antibiotic",
                    Dosage = "250mg",
                    Form = "Capsule",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/a/m/amoxicilin_500-_100_capsule-_carton_pic.jpg"
                },
                new Medication
                {
                    Name = "Paracetamol",
                    ActiveSubstance = "Paracetamol",
                    Category = "Analgesic",
                    Dosage = "500mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/9/8/9877_68.jpg"
                },
                new Medication
                {
                    Name = "Metformin",
                    ActiveSubstance = "Metformin",
                    Category = "Antidiabetic",
                    Dosage = "500mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/9/8/9877_68.jpg"
                },
                new Medication
                {
                    Name = "Losartan",
                    ActiveSubstance = "Losartan Potassium",
                    Category = "Antihypertensive",
                    Dosage = "50mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/f/i/file_184.jpg"
                },
                new Medication
                {
                    Name = "Simvastatin",
                    ActiveSubstance = "Simvastatin",
                    Category = "Statin",
                    Dosage = "10mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/3/5/35007_1114.jpg"
                },
                new Medication
                {
                    Name = "Omeprazole",
                    ActiveSubstance = "Omeprazole",
                    Category = "Proton Pump Inhibitor",
                    Dosage = "20mg",
                    Form = "Capsule",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/f/i/file_129.jpg"
                },
                new Medication
                {
                    Name = "Loranex",
                    ActiveSubstance = "Loratadine",
                    Category = "Antihistamine",
                    Dosage = "10mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/3/8/38188_469.jpg"
                },
                new Medication
                {
                    Name = "Ciprofloxacin",
                    ActiveSubstance = "Ciprofloxacin",
                    Category = "Antibiotic",
                    Dosage = "500mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/6/8/68837_2844.jpg"
                },
                new Medication
                {
                    Name = "Levothyroxine",
                    ActiveSubstance = "Levothyroxine Sodium",
                    Category = "Thyroid Medication",
                    Dosage = "50mcg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/8/7/87404_3195.jpg"
                },
                new Medication
                {
                    Name = "Reviwe",
                    ActiveSubstance = "Diphenhydramine",
                    Category = "Antihistamine",
                    Dosage = "25mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/6/8/68343_1586_1.jpg"
                },
                new Medication
                {
                    Name = "Pari-flo",
                    ActiveSubstance = "Fluoxetine",
                    Category = "Antidepressant",
                    Dosage = "20mg",
                    Form = "Capsule",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/5/6/56318_2500.jpg"
                },
                new Medication
                {
                    Name = "Hypp",
                    ActiveSubstance = "Hydrochlorothiazide",
                    Category = "Diuretic",
                    Dosage = "25mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/1/3/13933_609_1_.jpg"
                },
                new Medication
                {
                    Name = "Prenessa",
                    ActiveSubstance = "Prednisone",
                    Category = "Corticosteroid",
                    Dosage = "10mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/p/h/pharmaproductphoto_4__7.jpg"
                },
                new Medication
                {
                    Name = "forte",
                    ActiveSubstance = "Enalapril Maleate",
                    Category = "Antihypertensive",
                    Dosage = "10mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/1/7/1779_4.jpg"
                },
                new Medication
                {
                    Name = "etenol",
                    ActiveSubstance = "Furosemide",
                    Category = "Diuretic",
                    Dosage = "40mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/7/7/77594_68.jpg"
                },
                new Medication
                {
                    Name = "Tamsulosin",
                    ActiveSubstance = "Tamsulosin",
                    Category = "Alpha Blocker",
                    Dosage = "0.4mg",
                    Form = "Capsule",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/6/5/65656_437.jpg"
                },
                new Medication
                {
                    Name = "Fluticasone",
                    ActiveSubstance = "Fluticasone Propionate",
                    Category = "Steroid",
                    Dosage = "50mcg",
                    Form = "Inhaler",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/1/1/11577.jpg"
                },
                new Medication
                {
                    Name = "Naproxen",
                    ActiveSubstance = "Naproxen",
                    Category = "NSAID",
                    Dosage = "250mg",
                    Form = "Tablet",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/8/0/80170_3154_2_.jpg"
                },
                new Medication
                {
                    Name = "Doxycycline",
                    ActiveSubstance = "Doxycycline Hyclate",
                    Category = "Antibiotic",
                    Dosage = "100mg",
                    Form = "Capsule",
                    ImageURL = "https://app.psp.ge/media/catalog/product/cache/42af9c00153a725a54afd0b4cc1e9364/2/1/2103_121.jpg"
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
