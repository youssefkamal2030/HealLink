using System;

namespace HealLink.Contracts.Guardian.Requests
{
    public record AssignGuardianRequest(
        Guid PatientId,
        Guid GuardianUserId,
        string RelationshipToPatient
    );
}
