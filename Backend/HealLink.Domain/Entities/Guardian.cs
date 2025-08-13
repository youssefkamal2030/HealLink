using System;
using System.Collections.Generic;
using HealLink.Domain.Base;

namespace HealLink.Domain.Entities
{
    public class Guardian : Entity
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public string RelationshipToPatient { get; private set; }

        private readonly List<Guid> _patientIds = new();
        public IReadOnlyCollection<Guid> PatientIds => _patientIds.AsReadOnly();

        private Guardian() { }

        public Guardian(Guid userId, string relationshipToPatient)
        {
            UserId = userId;
            RelationshipToPatient = relationshipToPatient ?? throw new ArgumentNullException(nameof(relationshipToPatient));
        }

        public void UpdateRelationship(string relationshipToPatient)
        {
            RelationshipToPatient = relationshipToPatient ?? throw new ArgumentNullException(nameof(relationshipToPatient));
            UpdateTimestamp();
        }

        public void AddPatient(Guid patientId)
        {
            if (!_patientIds.Contains(patientId))
            {
                _patientIds.Add(patientId);
                UpdateTimestamp();
            }
        }

        public void RemovePatient(Guid patientId)
        {
            if (_patientIds.Contains(patientId))
            {
                _patientIds.Remove(patientId);
                UpdateTimestamp();
            }
        }
    }
}