namespace HealLink.Contracts.MedicalHistory.Requests
{
    public record UpdateMedicalHistoryRequest(
        string ChronicConditions,
        string Allergies,
        string CurrentMedications,
        string PreviousSurgeries,
        string FamilyHistory,
        string Notes,
        string? FileLink
    );
}
