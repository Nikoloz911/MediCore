using Microsoft.EntityFrameworkCore;
using MediCore.Models;
namespace MediCore.Data;
public class DataContext : DbContext
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Diagnosis> Diagnoses { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<LabResult> LabResults { get; set; }
    public DbSet<LabTest> LabTests { get; set; }
    public DbSet<MedicalRecord> MedicalRecords { get; set; }
    public DbSet<Medication> Medications { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionItem> PrescriptionItems { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"");
    }
}
