using System;

namespace HealLink.Contracts.Doctor.Responses
{
    public record DoctorSummaryDto(
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
