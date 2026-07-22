using HealLink.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealLink.Infrastructure.Data.Configurations;

public class OTPConfiguration : IEntityTypeConfiguration<OTP>
{
    public void Configure(EntityTypeBuilder<OTP> builder)
    {
        builder.Ignore(o => o.User);
    }
}
