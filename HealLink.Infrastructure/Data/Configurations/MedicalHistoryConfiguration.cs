using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class MedicalHistoryConfiguration : IEntityTypeConfiguration<MedicalHistory>
{
    public void Configure(EntityTypeBuilder<MedicalHistory> builder)
    {
        builder.ToTable("MedicalHistories");

        // CASCADE from Patient
        builder.HasOne<Patient>()
            .WithOne(p => p.MedicalHistory)
            .HasForeignKey<MedicalHistory>(m => m.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Owned entity
        builder.OwnsOne(m => m.Details);
    }
}
