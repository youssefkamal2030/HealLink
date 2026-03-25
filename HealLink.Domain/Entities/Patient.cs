using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] Patient does not raise domain events yet — PatientRegisteredEvent should be raised in the constructor.
    // TODO: [DDD] UploadTestResult/ConfirmMedicationReminder use UnauthorizedAccessException — replace with a domain-specific exception.
    public class Patient : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid? GuardianId { get; private set; }
         public Guardian Guardian { get; private set; }

        private readonly List<Guid> _doctorIds = new();
        public ICollection<DoctorPatientConnection> DoctorConnections { get; private set; } = [];
        public MedicalHistory MedicalHistory { get; private set; }
        private readonly List<TestResult> _testResults = new();
        private readonly List<MedicationReminder> _medicationReminders = [];

        private Patient() { }

        public Patient(Guid userId)
        {
            UserId = userId;
        }
      

        public void AssignGuardian(Guid guardianId)
        {
            GuardianId = guardianId;
            UpdateTimestamp();
        }

        public void RemoveGuardian()
        {
            GuardianId = null;
            UpdateTimestamp();
        }

      
        

        public IReadOnlyCollection<TestResult> TestResults => _testResults.AsReadOnly();
        public IReadOnlyCollection<MedicationReminder> MedicationReminders => _medicationReminders.AsReadOnly();

      

        public void UploadTestResult(TestResult result, Guid actingUserId)
        {
            if (actingUserId != UserId && actingUserId != GuardianId)
                throw new UnauthorizedAccessException("Only the patient or their guardian can upload test results.");
            _testResults.Add(result);
        }

        public void ConfirmMedicationReminder(Guid reminderId, Guid actingUserId)
        {
            if (actingUserId != UserId && actingUserId != GuardianId)
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
