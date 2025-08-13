using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Aggregates
{
    public class PrescriptionAggregate
    {
        public Prescription Prescription { get; private set; }
        private readonly List<MedicationDosage> _dosages = new();

        public IReadOnlyCollection<MedicationDosage> Dosages => _dosages.AsReadOnly();

        public PrescriptionAggregate(Prescription prescription, IEnumerable<MedicationDosage> dosages)
        {
            Prescription = prescription ?? throw new ArgumentNullException(nameof(prescription));
            if (dosages != null) _dosages.AddRange(dosages);
        }

        public void AddDosage(MedicationDosage dosage)
        {
            _dosages.Add(dosage);
            Prescription.AddMedication(dosage);
        }
    }
} 