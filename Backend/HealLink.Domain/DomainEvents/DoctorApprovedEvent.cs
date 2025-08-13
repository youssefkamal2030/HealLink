using System;
using HealLink.Domain.Base;

namespace HealLink.Domain.DomainEvents
{
    public class DoctorApprovedEvent : IDomainEvent
    {
        public Guid DoctorId { get; }
        public DateTime OccurredOn { get; }
        
        public DoctorApprovedEvent(Guid doctorId)
        {
            DoctorId = doctorId;
            OccurredOn = DateTime.UtcNow;
        }
    }
} 