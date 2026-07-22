using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class MedicationReminderConfiguration : IEntityTypeConfiguration<MedicationReminder>
{
    public void Configure(EntityTypeBuilder<MedicationReminder> builder)
    {
        builder.ToTable("MedicationReminders");

        // NO ACTION from Patient to prevent cascade cycle with Prescription
        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
