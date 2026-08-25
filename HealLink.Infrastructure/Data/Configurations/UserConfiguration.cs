using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        // Configure all DateTime properties to be stored as UTC
        builder.Property(u => u.LastLoginAt)
            .HasColumnType("datetime2")
            .HasConversion(
                v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);

        builder.Property(u => u.CreatedAt)
            .HasColumnType("datetime2")
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(u => u.UpdatedAt)
            .HasColumnType("datetime2")
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        // OTP — owned by User aggregate, tracked via the _otps backing field
        // One-to-many: User has many OTPs, OTP belongs to one User (no inverse navigation)
        builder.HasMany(u => u.OTPs)
            .WithOne()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OTP_User_UserId");
    }
}
