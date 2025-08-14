using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;

namespace HealLink.Domain.Aggregates
{
    public class PatientAggregate
    {
        public Patient Patient { get; private set; }
        public MedicalHistory MedicalHistory { get; private set; }
        private readonly List<TestResult> _testResults = new();
        private readonly List<MedicationReminder> _medicationReminders = new();

        public IReadOnlyCollection<TestResult> TestResults => _testResults.AsReadOnly();
        public IReadOnlyCollection<MedicationReminder> MedicationReminders => _medicationReminders.AsReadOnly();

        public PatientAggregate(Patient patient, MedicalHistory medicalHistory, IEnumerable<TestResult> testResults, IEnumerable<MedicationReminder> reminders)
        {
            Patient = patient ?? throw new ArgumentNullException(nameof(patient));
            MedicalHistory = medicalHistory ?? throw new ArgumentNullException(nameof(medicalHistory));
            if (testResults != null) _testResults.AddRange(testResults);
            if (reminders != null) _medicationReminders.AddRange(reminders);
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