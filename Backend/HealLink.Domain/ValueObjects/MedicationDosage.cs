using System;

namespace HealLink.Domain.ValueObjects
{
    public class MedicationDosage
    {
        public string MedicationName { get; private set; }
        public string Dosage { get; private set; }
        public string Instructions { get; private set; }
        public TimeSpan[] ScheduledTimes { get; private set; }
        
        public MedicationDosage(string medicationName, string dosage, string instructions, TimeSpan[] scheduledTimes)
        {
            MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
            Dosage = dosage ?? throw new ArgumentNullException(nameof(dosage));
            Instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));
            ScheduledTimes = scheduledTimes ?? throw new ArgumentNullException(nameof(scheduledTimes));
        }
    }
} 