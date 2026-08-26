using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        // OTP — owned by User aggregate, tracked via the _otps backing field
        // Explicitly map the backing field so EF can populate and track it correctly
        builder.Navigation(nameof(User.Otps))
            .HasField("_otps")
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        // One-to-many: User has many OTPs, OTP belongs to one User (no inverse navigation)
        builder.HasMany(u => u.Otps)
            .WithOne()
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_OTP_User_UserId");
    }
}
