using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] MedicalHistory constructor accepts a 'description' parameter but never assigns it — silent data loss.
    // TODO: [DDD] MedicalHistory manually sets Id and CreatedAt in the constructor, duplicating what Entity base already does in its constructor — remove these redundant assignments.
    // TODO: [DDD] Details (MedicalHistoryDetails value object) is never initialized in the constructor — entity can exist in an invalid state with a null Details property.
    // TODO: [AGGREGATE] MedicalHistory belongs inside PatientAggregate as an owned entity — it must never be mutated from outside the aggregate boundary. UpdateDetails() should only be callable through PatientAggregate, not directly on the entity from application layer code.
    public class MedicalHistory : Entity
    {
        public Guid PatientId { get; private set; }
        public MedicalHistoryDetails Details { get; private set; }
        public string? FileLink { get; private set; } = null;
        public MedicalHistoryType Type { get; private set; } = MedicalHistoryType.Medication;

        private MedicalHistory() { } // For EF

        public MedicalHistory(Guid patientId, MedicalHistoryType type, string? description = null, string? fileLink = null)
        {
            Id = Guid.NewGuid();
            PatientId = patientId;
            Type = type;
            FileLink = fileLink;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateDetails(MedicalHistoryDetails newDetails)
        {
            Details = newDetails ?? throw new ArgumentNullException(nameof(newDetails));
            UpdateTimestamp();
        }
    }
} 