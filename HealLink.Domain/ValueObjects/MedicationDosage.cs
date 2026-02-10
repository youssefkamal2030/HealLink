using System;

namespace HealLink.Domain.ValueObjects
{
    public class MedicationDosage
    {
        public string MedicationName { get; private set; }
        public DosageDetails Dosage { get; private set; }
        public TimeSpan[] ScheduledTimes { get; private set; }
        
        public MedicationDosage(string medicationName, DosageDetails dosage, TimeSpan[] scheduledTimes)
        {
            MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
            Dosage = dosage ?? throw new ArgumentNullException(nameof(dosage));
            ScheduledTimes = scheduledTimes ?? throw new ArgumentNullException(nameof(scheduledTimes));
        }
        
    }
} 