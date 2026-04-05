using System;
using System.Collections.Generic;
using HealLink.Domain.Base;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    
    // TODO: [DDD] UploadTestResult/ConfirmMedicationReminder use UnauthorizedAccessException — replace with a domain-specific exception.
    // TODO: [REFACTOR-P2] Create a domain-specific exception class HealLink.Domain/Exceptions/DomainUnauthorizedException.cs (extends Exception). Replace all UnauthorizedAccessException throws in Patient with it. Update ExceptionHandlingMiddleware to map DomainUnauthorizedException → 403 Forbidden.
    // TODO: [REFACTOR-P2] AddConnectedDoctor/RemoveConnectedDoctor throw generic Exception — replace with InvalidOperationException and a descriptive message consistent with the rest of the domain.
    // TODO: [REFACTOR-P3] Patient.Guardian navigation property is public with no setter — should be private set. Same for Patient.User. These are EF navigation properties and should not be assignable from outside the aggregate.
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

        //public void UpdateMedicalHistory(string chronicConditions, string allergies, string medications, string surgeries, string familyHistory, string notes)
        //{
        //    MedicalHistory.UpdateConditions(chronicConditions);
        //    MedicalHistory.UpdateAllergies(allergies);
        //    MedicalHistory.UpdateMedications(medications);
        //    MedicalHistory.UpdateNotes(notes);
        //}

        public void AddConnectedDoctor(Guid doctorId)
        {
           
            if (_connectedDoctorIds.Contains(doctorId))
                throw new Exception("Doctor Already Exists");
           _connectedDoctorIds.Add(doctorId);
            UpdateTimestamp();
        }
        public void RemoveConnectedDoctor(Guid doctorId)
        {


            if (!_connectedDoctorIds.Contains(doctorId))
                throw new Exception("No Doctor Found with this Id");
         _connectedDoctorIds.Remove(doctorId);
            UpdateTimestamp();
         
        }
    }

}
