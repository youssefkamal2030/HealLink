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
            if (string.IsNullOrWhiteSpace(medicationName))
                throw new ArgumentException("Medication name cannot be empty", nameof(medicationName));
            if (string.IsNullOrWhiteSpace(dosage))
                throw new ArgumentException("Dosage cannot be empty", nameof(dosage));
            if (string.IsNullOrWhiteSpace(instructions))
                throw new ArgumentException("Instructions cannot be empty", nameof(instructions));
            if (scheduledTimes == null || scheduledTimes.Length == 0)
                throw new ArgumentException("Scheduled times cannot be null or empty", nameof(scheduledTimes));
            
            MedicationName = medicationName;
            Dosage = dosage;
            Instructions = instructions;
            ScheduledTimes = scheduledTimes;
        }
        
        public override bool Equals(object obj)
        {
            if (obj is not MedicationDosage other) 
                return false;
            
            if (ScheduledTimes.Length != other.ScheduledTimes.Length) 
                return false;
            
            for (int i = 0; i < ScheduledTimes.Length; i++)
            {
                if (ScheduledTimes[i] != other.ScheduledTimes[i]) 
                    return false;
            }
            
            return MedicationName == other.MedicationName 
                && Dosage == other.Dosage 
                && Instructions == other.Instructions;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(MedicationName, Dosage, Instructions, ScheduledTimes.Length);
        }
    }
} 