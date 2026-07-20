using System;

namespace HealLink.Contracts.Doctor.Responses
{
    public record DoctorSummaryResponse(
        Guid DoctorId,
        Guid UserId,
        string Username,
        string Email,
        string? Specialization,
        string? PracticeLicenseNumber,
        string? SyndicateIdImagePath,
        DateTime CreatedAt
    );
}
