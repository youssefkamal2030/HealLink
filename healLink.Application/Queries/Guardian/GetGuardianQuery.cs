using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Guardian.Responses;
using MediatR;

namespace healLink.Application.Queries.Guardian
{
    public record GetGuardianQuery(Guid PatientId) : IRequest<Result<GuardianResponse>>;
}
