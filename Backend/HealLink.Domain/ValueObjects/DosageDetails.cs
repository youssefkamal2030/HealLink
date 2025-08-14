using System;

namespace HealLink.Domain.ValueObjects
{
    public class DosageDetails
    {
        public string MedicationName { get; }
        public string Dosage { get; }
        public string Instructions { get; }
        public TimeSpan[] ScheduledTimes { get; }

        public DosageDetails(string medicationName, string dosage, string instructions, TimeSpan[] scheduledTimes)
        {
            MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
            Dosage = dosage ?? throw new ArgumentNullException(nameof(dosage));
            Instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));
            ScheduledTimes = scheduledTimes ?? throw new ArgumentNullException(nameof(scheduledTimes));
        }

        public override bool Equals(object obj)
        {
            if (obj is not DosageDetails other) return false;
            if (ScheduledTimes.Length != other.ScheduledTimes.Length) return false;
            for (int i = 0; i < ScheduledTimes.Length; i++)
                if (ScheduledTimes[i] != other.ScheduledTimes[i]) return false;
            return MedicationName == other.MedicationName && Dosage == other.Dosage && Instructions == other.Instructions;
        }

        public override int GetHashCode() => HashCode.Combine(MedicationName, Dosage, Instructions, ScheduledTimes);
    }
} 