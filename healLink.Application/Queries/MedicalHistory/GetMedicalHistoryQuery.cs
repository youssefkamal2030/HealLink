using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.MedicalHistory.Responses;
using MediatR;

namespace healLink.Application.Queries.MedicalHistory
{
    public record GetMedicalHistoryQuery(Guid PatientId) : IRequest<Result<MedicalHistoryResponse>>;
}
