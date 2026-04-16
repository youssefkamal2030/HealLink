using System;
using System.Collections.Generic;

namespace HealLink.Contracts.Prescriptions.Responses
{
    public record PrescriptionResponse(
        Guid Id,
        Guid PatientId,
        Guid DoctorId,
        string Notes,
        string Status,
        DateTime? ExpiresAt,
        DateTime CreatedAt,
        List<MedicationDosageResponse> Medications
    );

    public record MedicationDosageResponse(
        string MedicationName,
        string Dosage,
        string Instructions,
        TimeSpan[] ScheduledTimes
    );

    public record PrescriptionsListResponse(List<PrescriptionResponse> Prescriptions);
}
