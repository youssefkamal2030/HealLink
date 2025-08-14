using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class MedicationMissedEvent : IDomainEvent
    {
        public Guid PatientId { get; }
        public Guid? GuardianId { get; }
        public string MedicationName { get; }
        public DateTime ScheduledTime { get; }
        public DateTime OccurredOn { get; }
        
        public MedicationMissedEvent(Guid patientId, Guid? guardianId, string medicationName, DateTime scheduledTime)
        {
            PatientId = patientId;
            GuardianId = guardianId;
            MedicationName = medicationName;
            ScheduledTime = scheduledTime;
            OccurredOn = DateTime.UtcNow;
        }
    }
} 