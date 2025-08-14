namespace HealLink.Domain.ValueObjects
{
    public class MedicalHistoryDetails
    {
        public string ChronicConditions { get; }
        public string Allergies { get; }
        public string CurrentMedications { get; }
        public string PreviousSurgeries { get; }
        public string FamilyHistory { get; }
        public string Notes { get; }

        public MedicalHistoryDetails(string chronicConditions, string allergies, string currentMedications, string previousSurgeries, string familyHistory, string notes)
        {
            ChronicConditions = chronicConditions ?? string.Empty;
            Allergies = allergies ?? string.Empty;
            CurrentMedications = currentMedications ?? string.Empty;
            PreviousSurgeries = previousSurgeries ?? string.Empty;
            FamilyHistory = familyHistory ?? string.Empty;
            Notes = notes ?? string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (obj is not MedicalHistoryDetails other) return false;
            return ChronicConditions == other.ChronicConditions && Allergies == other.Allergies && CurrentMedications == other.CurrentMedications && PreviousSurgeries == other.PreviousSurgeries && FamilyHistory == other.FamilyHistory && Notes == other.Notes;
        }

        public override int GetHashCode() => HashCode.Combine(ChronicConditions, Allergies, CurrentMedications, PreviousSurgeries, FamilyHistory, Notes);
    }
} 