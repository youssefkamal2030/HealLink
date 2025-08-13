using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class PatientRegisteredEvent : IDomainEvent
    {
        public Guid PatientId { get; }
        public DateTime OccurredOn { get; }
        
        public PatientRegisteredEvent(Guid patientId)
        {
            PatientId = patientId;
            OccurredOn = DateTime.UtcNow;
        }
    }
} 