using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
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