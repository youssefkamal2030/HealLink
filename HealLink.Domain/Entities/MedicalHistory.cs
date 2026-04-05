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
    // TODO: [DOMAIN-NEXT] Fix the constructor: remove the `Id = Guid.NewGuid()` and `CreatedAt = DateTime.UtcNow` lines — Entity base already sets these. Remove the unused `description` parameter or assign it to a field.
    // TODO: [DOMAIN-NEXT] Initialize Details in the constructor — either require it as a constructor parameter (MedicalHistoryDetails details) or initialize it to a default empty instance so the entity is never in an invalid state.
    public class MedicalHistory : Entity
    {
        public Guid PatientId { get; private set; }
        public MedicalHistoryDetails Details { get; private set; }
        public string? FileLink { get; private set; } = null;

        private MedicalHistory() { } // For EF

        public MedicalHistory(Guid patientId, string? description = null, string? fileLink = null)
        {
            
            PatientId = patientId;
            FileLink = fileLink;
           
        }

        public void UpdateDetails(MedicalHistoryDetails newDetails)
        {
            Details = newDetails ?? throw new ArgumentNullException(nameof(newDetails));
            UpdateTimestamp();
        }
    }
} 