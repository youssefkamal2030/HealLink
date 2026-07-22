using System.Reflection;
using healLink.Application.Interfaces;
using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Data;

public class HealLinkDbContext : DbContext, IApplicationDbContext, IDisposable
{
    public DbSet<User> Users { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Guardian> Guardians { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
    public DbSet<MedicalHistory> MedicalHistories { get; set; }
    public DbSet<MedicationReminder> MedicationReminders { get; set; }
    public DbSet<DoctorPatientConnection> DoctorPatientConnections { get; set; }
    public DbSet<OTP> OTPs { get; set; }

    public HealLinkDbContext(DbContextOptions<HealLinkDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all configurations from the Configurations folder by using reflection to scan the dll files to find all classes that implement IEntityTypeConfiguration<T> and apply them to the modelBuilder
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Ignore value objects
        modelBuilder.Ignore<HealLink.Domain.ValueObjects.MedicationDosage>();
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        base.Dispose();
    }
}