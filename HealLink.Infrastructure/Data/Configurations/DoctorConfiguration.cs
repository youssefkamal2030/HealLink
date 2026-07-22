using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        // CASCADE from User
        builder.HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Owned entities
        builder.OwnsOne(d => d.Address);
        builder.OwnsOne(d => d.PersonalInfo);   
        builder.OwnsOne(d => d.QRCode, qr =>
        {
            qr.Property(q => q.Value).HasColumnName("QRCode");
            qr.Property(q => q.GeneratedAt).HasColumnName("QRCodeGeneratedAt");
        });
        builder.OwnsOne(d => d.Rejection, r =>
        {
            r.Property(r => r.Reason).HasColumnName("RejectionReason");
            r.Property(r => r.RejectedAt).HasColumnName("RejectionDate");
        });
    }
}
