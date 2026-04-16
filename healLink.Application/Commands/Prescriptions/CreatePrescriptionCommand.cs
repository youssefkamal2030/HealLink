using System;
using System.Collections.Generic;
using healLink.Application.Common.Models;
using HealLink.Contracts.Prescriptions.Responses;
using HealLink.Domain.ValueObjects;
using MediatR;

namespace healLink.Application.Commands.Prescriptions
{
    public record CreatePrescriptionCommand(
        Guid DoctorId,
        Guid PatientId,
        string Notes,
        List<MedicationDosage> Medications,
        DateTime? ExpiresAt
    ) : IRequest<Result<PrescriptionResponse>>;
}
