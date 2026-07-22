using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class TestResultConfiguration : IEntityTypeConfiguration<TestResult>
{
    public void Configure(EntityTypeBuilder<TestResult> builder)
    {
        builder.ToTable("TestResults");

        // CASCADE from Patient
        builder.HasOne<Patient>()
            .WithMany()
            .HasForeignKey(t => t.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // SET NULL from Guardian
        builder.HasOne<Guardian>()
            .WithMany()
            .HasForeignKey(t => t.UploadedByGuardianId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
