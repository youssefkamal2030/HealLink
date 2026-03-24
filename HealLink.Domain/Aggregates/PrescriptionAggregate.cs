using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Aggregates
{
    // TODO: [AGGREGATE] PrescriptionAggregate is not a real aggregate — it is a redundant wrapper. Prescription already encapsulates its own _medications list with full invariant enforcement. This class should be deleted.
    // TODO: [AGGREGATE] _dosages list here mirrors Prescription._medications — AddDosage() writes to both, creating a dual-state consistency hazard. Any divergence between the two lists produces silent data corruption.
    // TODO: [AGGREGATE] Prescription entity should extend AggregateRoot directly and become the aggregate root. PrescriptionCreatedEvent exists in the domain but is never raised — it should be raised in Prescription's constructor once it extends AggregateRoot.
    // TODO: [AGGREGATE-MISSING] MedicationReminder list is missing — per BR-REM-01, reminders are generated from a prescription's medication schedule. The prescription aggregate is the natural owner: when a medication dosage is added, the corresponding reminders should be created here and a domain event raised. Currently reminders are orphaned in PatientAggregate with no link back to whether the source prescription is still active.
    // TODO: [AGGREGATE] The invariant from BR-PRE-01 (only a connected doctor can issue a prescription) cannot be enforced inside the aggregate — enforce at the application layer via a domain service, but the aggregate must at minimum guard that DoctorId and PatientId are non-empty Guids.
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