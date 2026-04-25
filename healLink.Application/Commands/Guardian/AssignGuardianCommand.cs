using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Guardian.Responses;
using MediatR;

namespace healLink.Application.Commands.Guardian
{
    public record AssignGuardianCommand(
        Guid PatientId,
        Guid GuardianUserId,
        string RelationshipToPatient
    ) : IRequest<Result<GuardianResponse>>;

    public record RemoveGuardianCommand(Guid PatientId) : IRequest<Result<bool>>;
}
