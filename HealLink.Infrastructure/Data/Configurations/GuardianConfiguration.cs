using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class GuardianConfiguration : IEntityTypeConfiguration<Guardian>
{
    public void Configure(EntityTypeBuilder<Guardian> builder)
    {
        // CASCADE from User
        builder.HasOne(g => g.User)
            .WithOne()
            .HasForeignKey<Guardian>(g => g.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
