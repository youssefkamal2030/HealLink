using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Profile
{
    public record PatientProfileResponse(
        Guid Id,
        Guid UserId,
        string FullName,
        string Email,
        Guid? GuardianId,
        string? GuardianName,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
