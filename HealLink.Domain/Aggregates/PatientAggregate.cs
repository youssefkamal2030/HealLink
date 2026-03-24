using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;

namespace HealLink.Domain.Aggregates
{
    // TODO: [AGGREGATE] PatientAggregate uses the wrapper pattern — Patient entity should be merged into this class and extend AggregateRoot directly.
    // TODO: [AGGREGATE] Patient and MedicalHistory are exposed as public properties — callers can bypass the aggregate boundary and mutate them directly; make access internal to the aggregate.
    // TODO: [AGGREGATE] DoctorPatientConnection list is duplicated here and in DoctorAggregate — a connection belongs to one boundary. Since the doctor accepts/rejects (BR-CON-04), connections are owned by DoctorAggregate. PatientAggregate should only hold a read-only list of connected DoctorIds, not full connection objects.
    // TODO: [AGGREGATE] UnauthorizedAccessException is a system exception used for a domain rule — replace with a domain-specific exception (e.g., DomainUnauthorizedException) in both UploadTestResult() and ConfirmMedicationReminder().
    // TODO: [AGGREGATE-MISSING] Guardian is missing — per BR-PAT-02, a patient can have at most one guardian (an invariant). Per BR-PAT-04, guardian permissions are scoped. The aggregate checks Patient.GuardianId for authorization but never loads the Guardian entity, so relationship type and guardian status cannot be validated.
    // TODO: [AGGREGATE-MISSING] Prescription list (read-only) is missing — per BR-REM-01, reminders are generated from active prescriptions. ConfirmMedicationReminder() cannot verify the source prescription is still active without it.
    // TODO: [AGGREGATE-MISSING] Notification collection is missing — patient-scoped notifications (BR-NOT-02: ConnectionAccepted, ConnectionRejected, PrescriptionCreated, MedicationMissed) are owned by this boundary and should be managed through the aggregate.
    public class PatientAggregate
    {
        public Patient Patient { get; private set; }
        public MedicalHistory MedicalHistory { get; private set; }
        private readonly List<TestResult> _testResults = new();
        private readonly List<MedicationReminder> _medicationReminders = new();
        private readonly List<DoctorPatientConnection> _connections = new();

        public IReadOnlyCollection<TestResult> TestResults => _testResults.AsReadOnly();
        public IReadOnlyCollection<MedicationReminder> MedicationReminders => _medicationReminders.AsReadOnly();
        public IReadOnlyCollection<DoctorPatientConnection> Connections => _connections.AsReadOnly();

        public PatientAggregate(Patient patient, MedicalHistory medicalHistory, IEnumerable<TestResult> testResults, IEnumerable<MedicationReminder> reminders, IEnumerable<DoctorPatientConnection> connections)
        {
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            MedicalHistory = medicalHistory ?? throw new ArgumentNullException(nameof(medicalHistory));
            if (testResults != null) _testResults.AddRange(testResults);
            if (reminders != null) _medicationReminders.AddRange(reminders);
            if (connections != null) _connections.AddRange(connections);
        }

        public void UploadTestResult(TestResult result, Guid actingUserId)
        {
            if (actingUserId != Patient.UserId && actingUserId != Patient.GuardianId)
                throw new UnauthorizedAccessException("Only the patient or their guardian can upload test results.");
            _testResults.Add(result);
        }

        public void ConfirmMedicationReminder(Guid reminderId, Guid actingUserId)
        {
            if (actingUserId != Patient.UserId && actingUserId != Patient.GuardianId)
                throw new UnauthorizedAccessException("Only the patient or their guardian can confirm reminders.");
            var reminder = _medicationReminders.Find(r => r.Id == reminderId);
            if (reminder == null) throw new InvalidOperationException("Reminder not found.");
            reminder.MarkAsTaken();
        }

        //public void UpdateMedicalHistory(string chronicConditions, string allergies, string medications, string surgeries, string familyHistory, string notes)
        //{
        //    MedicalHistory.UpdateConditions(chronicConditions);
        //    MedicalHistory.UpdateAllergies(allergies);
        //    MedicalHistory.UpdateMedications(medications);
        //    MedicalHistory.UpdateNotes(notes);
        //}
    }
}