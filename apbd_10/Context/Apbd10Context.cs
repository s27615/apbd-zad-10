
using apbd10.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd10.Context;

public class Apbd10Context : DbContext
{
    public Apbd10Context()
    {
    }
    
    public Apbd10Context(DbContextOptions options) : base(options) {
    }
    
    public DbSet<Doctor> Students { get; set; } //powinno byc Doctors
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }
}