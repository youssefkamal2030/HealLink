using System;
using System.Collections.Generic;

namespace HealLink.Contracts.Prescriptions.Requests
{
    public record CreatePrescriptionRequest(
        Guid DoctorId,
        Guid PatientId,
        string Notes,
        List<MedicationDosageDto> Medications,
        DateTime? ExpiresAt
    );

    public record MedicationDosageDto(
        string MedicationName,
        string Dosage,
        string Instructions,
        TimeSpan[] ScheduledTimes
    );
}
