using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class DoctorPatientConnectionConfiguration : IEntityTypeConfiguration<DoctorPatientConnection>
{
    public void Configure(EntityTypeBuilder<DoctorPatientConnection> builder)
    {
        builder.ToTable("DoctorPatientConnections");

        // CASCADE from Doctor
        builder.HasOne(c => c.Doctor)
            .WithMany(d => d.PatientConnections)
            .HasForeignKey(c => c.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        // RESTRICT from Patient
        builder.HasOne(c => c.Patient)
            .WithMany()
            .HasForeignKey(c => c.PatientId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
