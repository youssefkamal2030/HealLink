using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.MedicalHistory.Responses;
using MediatR;

namespace healLink.Application.Commands.MedicalHistory
{
    public record UpdateMedicalHistoryCommand(
        Guid PatientId,
        string ChronicConditions,
        string Allergies,
        string CurrentMedications,
        string PreviousSurgeries,
        string FamilyHistory,
        string Notes,
        string? FileLink
    ) : IRequest<Result<MedicalHistoryResponse>>;
}
