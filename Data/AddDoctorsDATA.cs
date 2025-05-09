using BCrypt.Net;
using MediCore.Enums;
using MediCore.Models;

namespace MediCore.Data
{
    public class AddDoctorsDATA
    {
        public static void InitializeData(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            SeedDoctorsData(context);
        }
        private static void SeedDoctorsData(DataContext context)
        {
            var doctors = new List<Doctor>
            {
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "James",
                        LastName = "Smith",
                        Email = "james.smith@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 1,
                    Specialty = "Cardiology",
                    LicenseNumber = "LN001",
                    WorkingHours = "9AM - 5PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/7ulpUX2Tm4MYgQHRjfzHpB5pyhJiy3SsmYgBcdrR.webp",
                    ExperienceYears = 6,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Mary",
                        LastName = "Johnson",
                        Email = "mary.johnson@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 2,
                    Specialty = "Neurology",
                    LicenseNumber = "LN002",
                    WorkingHours = "9AM - 4PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/iECPsw0oTISowqkcZxpWSWiAzJQKsgirKUb86HRp.webp",
                    ExperienceYears = 8,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Robert",
                        LastName = "Williams",
                        Email = "robert.williams@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 3,
                    Specialty = "Orthopedics",
                    LicenseNumber = "LN003",
                    WorkingHours = "8AM - 5PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/4OLsth5lrbnW5RVzEos94hhCpahvu1Rjj3clrSxo.webp",
                    ExperienceYears = 10,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Patricia",
                        LastName = "Brown",
                        Email = "patricia.brown@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 4,
                    Specialty = "Pediatrics",
                    LicenseNumber = "LN004",
                    WorkingHours = "10AM - 6PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/jJBicceDLAVgLvpozBm85HRu06fMfYfLbtEibwNT.webp",
                    ExperienceYears = 5,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Michael",
                        LastName = "Jones",
                        Email = "michael.jones@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 5,
                    Specialty = "Dermatology",
                    LicenseNumber = "LN005",
                    WorkingHours = "9AM - 3PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/7CxKoikCmM4ICFM4cdveZ3v54t3rCTaeX9gfi6e5.webp",
                    ExperienceYears = 7,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Linda",
                        LastName = "Garcia",
                        Email = "linda.garcia@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 7,
                    Specialty = "Gastroenterology",
                    LicenseNumber = "LN006",
                    WorkingHours = "8AM - 4PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/aJEEXJp5GrhwF3mlrv17W5Gf7FP2TYVaMFxjN75a.webp",
                    ExperienceYears = 9,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "David",
                        LastName = "Martinez",
                        Email = "david.martinez@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 6,
                    Specialty = "Psychiatry",
                    LicenseNumber = "LN007",
                    WorkingHours = "9AM - 5PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/f1a7x8BAwNyPaOQwCo9iN64J27JR3rYdCAkLkFze.webp",
                    ExperienceYears = 11,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Susan",
                        LastName = "Hernandez",
                        Email = "susan.hernandez@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 8,
                    Specialty = "Radiology",
                    LicenseNumber = "LN008",
                    WorkingHours = "7AM - 3PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/bV9jm5UOHlB1hAKMC1D6qNLPTPrPKUN934XsTOJ8.webp",
                    ExperienceYears = 4,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Joseph",
                        LastName = "Lopez",
                        Email = "joseph.lopez@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 1,
                    Specialty = "Cardiology",
                    LicenseNumber = "LN009",
                    WorkingHours = "8AM - 6PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/LrANPnmCKOGyxzPiT06tjfXgo4AkpBtpo6iY85YB.webp",
                    ExperienceYears = 12,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Karen",
                        LastName = "Wilson",
                        Email = "karen.wilson@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 4,
                    Specialty = "Pediatrics",
                    LicenseNumber = "LN010",
                    WorkingHours = "9AM - 4PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/bV9jm5UOHlB1hAKMC1D6qNLPTPrPKUN934XsTOJ8.webp",
                    ExperienceYears = 7,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Daniel",
                        LastName = "Evans",
                        Email = "daniel.evans@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 6,
                    Specialty = "Psychiatry",
                    LicenseNumber = "LN011",
                    WorkingHours = "8AM - 5PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/o6JzOOBeRbGwFhgWdEK2U9nJ5ifadEatCBpGpMtv.webp",
                    ExperienceYears = 5,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Emma",
                        LastName = "Clark",
                        Email = "emma.clark@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 8,
                    Specialty = "Radiology",
                    LicenseNumber = "LN012",
                    WorkingHours = "9AM - 3PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/f1a7x8BAwNyPaOQwCo9iN64J27JR3rYdCAkLkFze.webp",
                    ExperienceYears = 6,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Christopher",
                        LastName = "Lopez",
                        Email = "christopher.lopez@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 3,
                    Specialty = "Orthopedics",
                    LicenseNumber = "LN013",
                    WorkingHours = "8AM - 4PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/OPodK2ZEa9b1iUWjQTN5DtwwnxSTyyUuF7R4EzMq.webp",
                    ExperienceYears = 9,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Olivia",
                        LastName = "Walker",
                        Email = "olivia.walker@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 1,
                    Specialty = "Cardiology",
                    LicenseNumber = "LN014",
                    WorkingHours = "9AM - 5PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/lNQzKixRl0SGWMwA3Owdxh2fhiAwYoOLLCWAdcyp.jpg",
                    ExperienceYears = 7,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Matthew",
                        LastName = "Hall",
                        Email = "matthew.hall@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 4,
                    Specialty = "Pediatrics",
                    LicenseNumber = "LN015",
                    WorkingHours = "8AM - 5PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/0L7UbvkVSSGyuJLaxN6ChoJSR7taiwbyyJHjeJKv.webp",
                    ExperienceYears = 10,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Isabella",
                        LastName = "Allen",
                        Email = "isabella.allen@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 2,
                    Specialty = "Neurology",
                    LicenseNumber = "LN016",
                    WorkingHours = "9AM - 4PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/EB1Okwwjp3rj5DRXlTj6B5bYrtlUXD84mM7LYw6q.webp",
                    ExperienceYears = 6,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "James",
                        LastName = "Young",
                        Email = "james.young@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 6,
                    Specialty = "Psychiatry",
                    LicenseNumber = "LN017",
                    WorkingHours = "8AM - 6PM",
                    ImageURL = "https://dashboard.aversiclinic.ge/storage/doctors/VdBeH0vNG0FVGV0NpHNmBEIVQlHrjg928JSkJY5H.jpg",
                    ExperienceYears = 8,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Sophia",
                        LastName = "King",
                        Email = "sophia.king@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 5,
                    Specialty = "Dermatology",
                    LicenseNumber = "LN018",
                    WorkingHours = "10AM - 6PM",
                    ImageURL    = "https://dashboard.aversiclinic.ge/storage/doctors/xMQYj7sN33SePT1IEt55aO3YtYrSvK1unskXVyy7.jpg",
                    ExperienceYears = 4,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Lucas",
                        LastName = "Scott",
                        Email = "lucas.scott@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 7,
                    Specialty = "Gastroenterology",
                    LicenseNumber = "LN019",
                    WorkingHours = "8AM - 5PM",
                    ImageURL   = "https://dashboard.aversiclinic.ge/storage/doctors/4F2NENHDc4EP2KZCKLtYVjmzFBJWwMWpqJVM2asP.jpg",
                    ExperienceYears = 7,
                },
                new Doctor
                {
                    User = new User
                    {
                        FirstName = "Ava",
                        LastName = "Green",
                        Email = "ava.green@example.com",
                        Password = "password123",
                        Role = USER_ROLE.DOCTOR,
                        Status = USER_STATUS.ACTIVE,
                    },
                    DepartmentId = 3,
                    Specialty = "Orthopedics",
                    LicenseNumber = "LN020",
                    WorkingHours = "9AM - 5PM",
                    ImageURL  = "https://dashboard.aversiclinic.ge/storage/doctors/m8AOkLPxYYmrbIJRUOysXEwV3IKqN7EFkjx0Va0n.jpg",
                    ExperienceYears = 5,
                },
            };

            foreach (var doctor in doctors)
            {
                doctor.User.Password = BCrypt.Net.BCrypt.HashPassword(doctor.User.Password);
                var existingDoctor = context.Doctors.FirstOrDefault(d =>
                    d.User.Email.ToLower() == doctor.User.Email.ToLower()
                );
                if (existingDoctor == null)
                {
                    context.Doctors.Add(doctor);
                }
            }
            context.SaveChanges();
        }
    }
}