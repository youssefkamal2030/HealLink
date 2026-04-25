using System;

namespace HealLink.Contracts.Guardian.Responses
{
    public record GuardianResponse(
        Guid Id,
        Guid UserId,
        string RelationshipToPatient,
        DateTime CreatedAt
    );
}
