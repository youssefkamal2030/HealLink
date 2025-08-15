using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Contracts.Profile
{
    public record DoctorProfileResponse(
        Guid Id,
        Guid UserId,
        string FullName,
        string Email,
        string Phone,
        string Specialization,
        string CurrentWorkplace,
        string LicenseNumber,
        string PracticeLicenseNumber,
        string Address,
        bool IsApproved,
        bool IsAvailableForChat,
        DateTime CreatedAt,
        DateTime UpdatedAt
    );
}
