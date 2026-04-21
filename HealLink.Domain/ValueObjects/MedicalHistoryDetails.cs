namespace HealLink.Domain.ValueObjects
{
    public class MedicalHistoryDetails
    {
        public string ChronicConditions { get; private set; }
        public string Allergies { get; private set; }
        public string CurrentMedications { get; private set; }
        public string PreviousSurgeries { get; private set; }
        public string FamilyHistory { get; private set; }
        public string Notes { get; private set; }

        private MedicalHistoryDetails() { } // For EF

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