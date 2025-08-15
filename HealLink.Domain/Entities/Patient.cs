using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class Patient : Entity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid? GuardianId { get; private set; }
         public Guardian Guardian { get; private set; }

        private readonly List<Guid> _doctorIds = new();
        public IReadOnlyCollection<Guid> DoctorIds => _doctorIds.AsReadOnly();

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
