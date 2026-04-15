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

        private MedicalHistory() { } // For EF

        public MedicalHistory(Guid patientId, string? fileLink = null, MedicalHistoryDetails? details = null)
        {
            
            PatientId = patientId;
            FileLink = fileLink;
            Details = details ?? new MedicalHistoryDetails(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
            
        }

        internal void UpdateDetails(MedicalHistoryDetails newDetails)
        {
            Details = newDetails ?? throw new ArgumentNullException(nameof(newDetails));
            UpdateTimestamp();
        }
    }
} 