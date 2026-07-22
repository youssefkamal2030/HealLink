using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.ToTable("Prescriptions");

        // CASCADE from Doctor
        builder.HasOne<Doctor>()
            .WithMany()
            .HasForeignKey(p => p.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // RESTRICT from Patient
        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        // Reminders collection (CASCADE)
        builder.HasMany<MedicationReminder>("_reminders")
            .WithOne()
            .HasForeignKey(r => r.PrescriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Ignore(r => r.Reminders);
    }
}
