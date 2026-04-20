using System;

namespace HealLink.Contracts.MedicalHistory.Responses
{
    public record MedicalHistoryResponse(
        Guid PatientId,
        string ChronicConditions,
        string Allergies,
        string CurrentMedications,
        string PreviousSurgeries,
        string FamilyHistory,
        string Notes,
        string? FileLink,
        DateTime UpdatedAt
    );
}
