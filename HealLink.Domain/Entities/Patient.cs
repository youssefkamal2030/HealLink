using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

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

        private Patient(Guid userId)
        {
            UserId = userId;
            AddDomainEvent(new PatientRegisteredEvent(Id));
        }
        
        /// <summary>
        /// Factory method for registering a new patient. Raises <see cref="PatientRegisteredEvent"/>.
        /// </summary>
        public static Patient Register(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));

            return new Patient(userId);
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
        // [PENDING] TODO: [REFACTOR-AUTH] Remove authorization logic from domain entity after centralized-authorization-infrastructure is complete
        // PROBLEM: Domain entity is handling authorization (checking actingUserId against UserId/GuardianId)
        //          This violates Clean Architecture - domain should only contain business rules
        // SPEC: .kiro/specs/centralized-authorization-infrastructure (In Progress)
        // MIGRATION: After spec implementation:
        //   1. Remove actingUserId parameter from UploadTestResult() and ConfirmMedicationReminder()
        //   2. Remove UnauthorizedAccessException throws
        //   3. Add [Authorize(AuthorizationPolicies.PatientOrGuardianAccess)] to related commands
        //   4. PatientOrGuardianAccessPolicy will be created per design.md
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

     
        public void UpdateMedicalHistory( MedicalHistoryDetails newDetails)
        {
          
          MedicalHistory.UpdateDetails(newDetails);

            UpdateTimestamp();
        }
        public void UpdateMedicalHistoryDetails(MedicalHistoryDetails details, string? fileLink = null)
        {
            if (details == null) throw new ArgumentNullException(nameof(details));

            if (MedicalHistory == null)
                MedicalHistory = new MedicalHistory(Id, fileLink, details);
            else
                MedicalHistory.UpdateDetails(details);

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
