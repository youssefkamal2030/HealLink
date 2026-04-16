using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class Patient : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public Guid? GuardianId { get; private set; }
         public Guardian Guardian { get; private set; }

        private readonly List<Guid> _connectedDoctorIds = [];
        private readonly List<TestResult> _testResults = [];
        private readonly List<MedicationReminder> _medicationReminders = [];
        public IReadOnlyCollection<TestResult> TestResults => _testResults;
        public IReadOnlyCollection<MedicationReminder> MedicationReminders => _medicationReminders;
        public IReadOnlyCollection<Guid> ConnectedDoctorIds => _connectedDoctorIds;

        public MedicalHistory MedicalHistory { get; private set; }
       

        private Patient() { }

        public Patient(Guid userId)
        {
            UserId = userId;
            AddDomainEvent(new PatientRegisteredEvent(Id));
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

        public void UpdateMedicalHistory(MedicalHistory medicalHistory )
        {
            MedicalHistory = medicalHistory ?? throw new ArgumentNullException(nameof(medicalHistory));
            UpdateTimestamp();

        }

        public void AddConnectedDoctor(Guid doctorId)
        {
            if (_connectedDoctorIds.Contains(doctorId))
                throw new InvalidOperationException("Doctor is already connected to this patient.");
            _connectedDoctorIds.Add(doctorId);
            UpdateTimestamp();
        }

        public void RemoveConnectedDoctor(Guid doctorId)
        {
            if (!_connectedDoctorIds.Contains(doctorId))
                throw new InvalidOperationException("Doctor is not connected to this patient.");
            _connectedDoctorIds.Remove(doctorId);
            UpdateTimestamp();
        }
    }

}
