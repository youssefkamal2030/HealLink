using System;
using System.Collections.Generic;

namespace HealLink.Contracts.Reminders.Responses
{
    public record MedicationReminderResponse(
        Guid Id,
        Guid PatientId,
        Guid PrescriptionId,
        string MedicationName,
        DateTime ScheduledTime,
        string Status,
        DateTime? TakenAt
    );

    public record MedicationRemindersListResponse(List<MedicationReminderResponse> Reminders);
}
