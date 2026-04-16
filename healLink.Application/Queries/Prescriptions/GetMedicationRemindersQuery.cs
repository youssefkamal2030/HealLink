using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Reminders.Responses;
using HealLink.Domain.Enums;
using MediatR;

namespace healLink.Application.Queries.Prescriptions
{
    public record GetMedicationRemindersQuery(
        Guid PatientId,
        DateTime? Date = null,
        MedicationReminderStatus? Status = null
    ) : IRequest<Result<MedicationRemindersListResponse>>;
}
