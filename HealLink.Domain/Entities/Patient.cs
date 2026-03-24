using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] Patient does not extend AggregateRoot — it cannot raise domain events (e.g., PatientRegisteredEvent should be raised here).
    // TODO: [DDD] DoctorConnections has a public setter — external code can replace the entire collection, violating encapsulation.
    // TODO: [DDD] _doctorIds list is maintained in parallel with DoctorConnections navigation property — dual state management risks inconsistency; pick one source of truth.
    // TODO: [DDD] ConnectToDoctor/DisconnectFromDoctor mutate _doctorIds but don't raise domain events for these significant state changes.
    public class Patient : Entity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid? GuardianId { get; private set; }
         public Guardian Guardian { get; private set; }

        private readonly List<Guid> _doctorIds = new();
        public IReadOnlyCollection<Guid> DoctorIds => _doctorIds.AsReadOnly();
        public ICollection<DoctorPatientConnection> DoctorConnections { get; set; } = new List<DoctorPatientConnection>();

        private Patient() { } // For EF

        public Patient(Guid userId)
        {
            UserId = userId;
        }

        public void AssignGuardian(Guid guardianId)
        {
            GuardianId = guardianId;
            UpdateTimestamp();
        }

        public void RemoveGuardian()
        {
            GuardianId = null;
            UpdateTimestamp();
        }

        public void ConnectToDoctor(Guid doctorId)
        {
            if (!_doctorIds.Contains(doctorId))
            {
                _doctorIds.Add(doctorId);
                UpdateTimestamp();
            }
        }

        public void DisconnectFromDoctor(Guid doctorId)
        {
            if (_doctorIds.Contains(doctorId))
            {
                _doctorIds.Remove(doctorId);
                UpdateTimestamp();
            }
        }
    }
}
