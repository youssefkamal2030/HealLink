using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    //this should not be an aggregate root, it should be an owned entity  owned by the patient aggregate root since this medical history has no meaning outside of the patient aggregate root, and it is not a root entity that can be accessed independently.
    // so the solution is to make this as internal entity inside the patient aggregate root, and make it an owned entity in the patient aggregate root, and remove the repository for this entity, and make it accessible only through the patient aggregate root.
    // the only trade-off when we load the patient aggregate root, we will load the medical history as well which might be a performance issue if the medical history is large, but we can solve this by using lazy loading or by using a separate query to load the medical history when needed.
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
        internal void UpdateFileLink(string? newFileLink)
        {
            FileLink = newFileLink;
            UpdateTimestamp();
        }
    }
} 