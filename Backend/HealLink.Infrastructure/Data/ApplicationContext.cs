using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Data
{
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

        public HealLinkDbContext(DbContextOptions<HealLinkDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>().ToTable("Users");

            // Patient - CASCADE from User, NO ACTION to Guardian
            modelBuilder.Entity<Patient>()
                .HasOne(p => p.User)
                .WithOne()
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Guardian)
                .WithMany()
                .HasForeignKey(p => p.GuardianId)
                .OnDelete(DeleteBehavior.NoAction); // Changed to NoAction

            // Doctor - CASCADE from User
            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.User)
                .WithOne()
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Doctor>()
                .OwnsOne(d => d.Address);

            modelBuilder.Entity<Doctor>()
                .OwnsOne(d => d.PersonalInfo);

            // Guardian - CASCADE from User
            modelBuilder.Entity<Guardian>()
                .HasOne(g => g.User)
                .WithOne()
                .HasForeignKey<Guardian>(g => g.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Patient-related entities - CASCADE from Patient
            modelBuilder.Entity<MedicalHistory>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TestResult>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MedicationReminder>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // Doctor-Patient Connection - CASCADE from Doctor, RESTRICT from Patient
            modelBuilder.Entity<DoctorPatientConnection>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DoctorPatientConnection>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor-related entities - CASCADE from Doctor
            modelBuilder.Entity<Prescription>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Prescription>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Subscription>()
                .HasOne<Doctor>()
                .WithMany()
                .HasForeignKey(s => s.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                .HasOne<Patient>()
                .WithMany()
                .HasForeignKey(s => s.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Medication Reminder - Prescription relationship
            modelBuilder.Entity<MedicationReminder>()
                .HasOne<Prescription>()
                .WithMany()
                .HasForeignKey(m => m.PrescriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Chat Messages - RESTRICT to prevent cascade conflicts
            modelBuilder.Entity<ChatMessage>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChatMessage>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Notifications - CASCADE from User
            modelBuilder.Entity<Notification>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // TestResult - Guardian relationship
            modelBuilder.Entity<TestResult>()
                .HasOne<Guardian>()
                .WithMany()
                .HasForeignKey(t => t.UploadedByGuardianId)
                .OnDelete(DeleteBehavior.SetNull);

            // Payment - Subscription relationship
            modelBuilder.Entity<Payment>()
                .HasOne<Subscription>()
                .WithMany()
                .HasForeignKey(p => p.SubscriptionId)
                .OnDelete(DeleteBehavior.SetNull);

            // Table configurations
            modelBuilder.Entity<Prescription>().ToTable("Prescriptions");
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
            modelBuilder.Entity<Notification>().ToTable("Notifications")
                .Ignore(N => N.Data);
            modelBuilder.Entity<ChatMessage>().ToTable("ChatMessages");
            modelBuilder.Entity<TestResult>().ToTable("TestResults");
            modelBuilder.Entity<MedicalHistory>()
                .ToTable("MedicalHistories")
                .Ignore(m => m.Type)
                .Ignore(m => m.Details);
            modelBuilder.Entity<MedicationReminder>().ToTable("MedicationReminders");
            modelBuilder.Entity<DoctorPatientConnection>().ToTable("DoctorPatientConnections");

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
}