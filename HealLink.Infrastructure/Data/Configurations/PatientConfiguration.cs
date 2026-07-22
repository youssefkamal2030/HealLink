using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        // CASCADE from User
        builder.HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Patient>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // NO ACTION to Guardian to prevent cascade cycles
        builder.HasOne(p => p.Guardian)
            .WithMany()
            .HasForeignKey(p => p.GuardianId)
            .OnDelete(DeleteBehavior.NoAction);

        
    }
}
