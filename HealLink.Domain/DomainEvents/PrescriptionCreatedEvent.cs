using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class PrescriptionCreatedEvent : IDomainEvent
    {
        public Guid PrescriptionId { get; }
        public Guid PatientId { get; }
        public DateTime OccurredOn { get; }
        
        public PrescriptionCreatedEvent(Guid prescriptionId, Guid patientId)
        {
            PrescriptionId = prescriptionId;
            PatientId = patientId;
            OccurredOn = DateTime.UtcNow;
        }
    }
} 