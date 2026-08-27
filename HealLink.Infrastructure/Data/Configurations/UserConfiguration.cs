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
        //builder.Property(u => u.LastLoginAt)
        //    .HasColumnType("datetime2")
        //    .HasConversion(
        //        v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
        //        v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : null);

        //builder.Property(u => u.CreatedAt)
        //    .HasColumnType("datetime2")
        //    .HasConversion(
        //        v => v.ToUniversalTime(),
        //        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        //builder.Property(u => u.UpdatedAt)
        //    .HasColumnType("datetime2")
        //    .HasConversion(
        //        v => v.ToUniversalTime(),
        //        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));


        /*
         * this line access the medata of the user entity and set it's property access mode from
         * PropertyAccessMode.PreferProperty (the default ) which uses the default developer defined getters and setters.
         * since the otp's feild is a readonly storage feild, we need to set the access mode to PropertyAccessMode.Field which will use the backing field directly.
         * 
         */
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
