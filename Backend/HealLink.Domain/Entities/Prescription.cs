using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    public class Prescription : Entity
    {
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public string Instructions { get; private set; }
        public string Notes { get; private set; }
        public PrescriptionStatus Status { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        private readonly List<MedicationDosage> _medications = new();
        public IReadOnlyCollection<MedicationDosage> Medications => _medications.AsReadOnly();

        private Prescription() { } 

        public Prescription(Guid patientId, Guid doctorId, string instructions, string notes, List<MedicationDosage> medications, DateTime? expiresAt = null)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            Instructions = instructions ?? string.Empty;
            Notes = notes ?? string.Empty;
            Status = PrescriptionStatus.Active;
            ExpiresAt = expiresAt;

            if (medications != null)
            {
                _medications.AddRange(medications);
            }
        }

        public void UpdateInstructions(string instructions)
        {
            Instructions = instructions ?? string.Empty;
            UpdateTimestamp();
        }

        public void UpdateNotes(string notes)
        {
            Notes = notes ?? string.Empty;
            UpdateTimestamp();
        }

        public void Activate()
        {
            Status = PrescriptionStatus.Active;
            UpdateTimestamp();
        }

        public void Deactivate()
        {
            Status = PrescriptionStatus.Inactive;
            UpdateTimestamp();
        }

        public void Expire()
        {
            Status = PrescriptionStatus.Expired;
            UpdateTimestamp();
        }

        public void AddMedication(MedicationDosage medication)
        {
            _medications.Add(medication);
            UpdateTimestamp();
        }

        public void RemoveMedication(MedicationDosage medication)
        {
            if (_medications.Contains(medication))
            {
                _medications.Remove(medication);
                UpdateTimestamp();
            }
        }
    }
}
