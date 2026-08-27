using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class OTPConfiguration : IEntityTypeConfiguration<OTP>
{
    public void Configure(EntityTypeBuilder<OTP> builder)
    {
        builder.ToTable("OTPs");
      
        // Configure ExpiryTime to be stored as UTC in the database
        //// This ensures all datetime comparisons use consistent UTC timestamps
        //builder.Property(o => o.ExpiryTime)
        //    .HasColumnType("datetime2")
        //    .HasConversion(
        //        v => v.ToUniversalTime(),
        //        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        //// Configure CreatedAt to be stored as UTC
        //builder.Property(o => o.CreatedAt)
        //    .HasColumnType("datetime2")
        //    .HasConversion(
        //        v => v.ToUniversalTime(),
        //        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        //// Configure UpdatedAt to be stored as UTC
        //builder.Property(o => o.UpdatedAt)
        //    .HasColumnType("datetime2")
        //    .HasConversion(
        //        v => v.ToUniversalTime(),
        //        v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
    }
}
