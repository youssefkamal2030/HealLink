﻿using System;
using System.Collections.Generic;
using System.Linq;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    public class Prescription : Entity
    {
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
      
        public string Notes { get; private set; }
        public PrescriptionStatus Status { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        private readonly List<MedicationDosage> _medications = new();
        public IReadOnlyCollection<MedicationDosage> Medications => _medications.AsReadOnly();

        private Prescription() { } 

        public Prescription(Guid patientId, Guid doctorId, string notes, List<MedicationDosage> medications, DateTime? expiresAt = null)
        {
            if (medications == null || medications.Count == 0)
                throw new InvalidOperationException("Prescription must contain at least one medication");
            
            if (expiresAt.HasValue && expiresAt.Value < DateTime.UtcNow)
                throw new ArgumentException("Expiration date cannot be in the past", nameof(expiresAt));
            
            PatientId = patientId;
            DoctorId = doctorId;
            Notes = notes ?? string.Empty;
            Status = PrescriptionStatus.Active;
            ExpiresAt = expiresAt;
            _medications.AddRange(medications);
        }

        public void UpdateInstructions(MedicationDosage medicationDosage)
        {
            var existingMedication = _medications.Find(m => m.MedicationName == medicationDosage.MedicationName);
            if (existingMedication != null)
            {
                _medications.Remove(existingMedication);
                _medications.Add(medicationDosage);
            }
           
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
            if (medication == null)
                throw new ArgumentNullException(nameof(medication));

            if (_medications.Any(m => m.MedicationName == medication.MedicationName))
                throw new InvalidOperationException($"Medication '{medication.MedicationName}' already exists in prescription");

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
