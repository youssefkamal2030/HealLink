using System;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using Xunit;

namespace HealLink.Domain.Tests.Entities
{
    // [TEST-COVERAGE] These TODOs indicate future test coverage improvements
    // CURRENT-STATUS: Basic tests exist for core functionality
    // [TEST-NEXT-1] Add tests for ConfirmMedicationReminder():
    //   - Patient can confirm their own reminder → Status becomes Taken
    //   - Guardian can confirm a reminder for their patient → Status becomes Taken
    //   - Third party throws UnauthorizedAccessException
    //   - Non-existent reminderId throws InvalidOperationException
    //   Note: Patient._medicationReminders is loaded via EF backing field — in unit tests you must
    //   use GetByPatientIdWithRemindersAsync() pattern or construct the patient with reminders directly.
    // [TEST-NEXT-2] Add tests for UpdateMedicalHistoryDetails():
    //   - First call creates a new MedicalHistory on the patient
    //   - Second call updates the existing one (same Id, different Details)
    //   - Null details throws ArgumentNullException
    // [TEST-NEXT-3] Add a test for AddConnectedDoctor_ThenRemove_ThenAddAgain — verify the same doctor ID
    //   can be re-added after being removed (no stale state).
    // [TEST-NEXT-4] Once UploadTestResult/ConfirmMedicationReminder are updated to throw a domain-specific
    //   exception instead of UnauthorizedAccessException (after centralized-authorization-infrastructure),
    //   update these tests to assert the new exception type or remove authorization checks entirely.
    public class PatientAggregateTests
    {
        private Patient CreatePatient() => Patient.Register(Guid.NewGuid());

        // ── Constructor ──────────────────────────────────────────────────────

        [Fact]
        public void Constructor_RaisesPatientRegisteredEvent()
        {
            var patient = CreatePatient();

            Assert.Single(patient.DomainEvents);
            Assert.IsType<PatientRegisteredEvent>(patient.DomainEvents.First());
        }

        [Fact]
        public void Constructor_SetsUserId()
        {
            var userId = Guid.NewGuid();
            var patient = Patient.Register(userId);

            Assert.Equal(userId, patient.UserId);
        }

        // ── AddConnectedDoctor ───────────────────────────────────────────────

        [Fact]
        public void AddConnectedDoctor_AddsToCollection()
        {
            var patient = CreatePatient();
            var doctorId = Guid.NewGuid();

            patient.AddConnectedDoctor(doctorId);

            Assert.Contains(doctorId, patient.ConnectedDoctorIds);
        }

        [Fact]
        public void AddConnectedDoctor_WithDuplicateId_ThrowsException()
        {
            var patient = CreatePatient();
            var doctorId = Guid.NewGuid();
            patient.AddConnectedDoctor(doctorId);

            Assert.ThrowsAny<Exception>(() => patient.AddConnectedDoctor(doctorId));
        }

        [Fact]
        public void AddConnectedDoctor_UpdatesTimestamp()
        {
            var patient = CreatePatient();
            var before = patient.UpdatedAt;
            System.Threading.Thread.Sleep(10);

            patient.AddConnectedDoctor(Guid.NewGuid());

            Assert.True(patient.UpdatedAt > before);
        }

        // ── RemoveConnectedDoctor ────────────────────────────────────────────

        [Fact]
        public void RemoveConnectedDoctor_RemovesFromCollection()
        {
            var patient = CreatePatient();
            var doctorId = Guid.NewGuid();
            patient.AddConnectedDoctor(doctorId);

            patient.RemoveConnectedDoctor(doctorId);

            Assert.DoesNotContain(doctorId, patient.ConnectedDoctorIds);
        }

        [Fact]
        public void RemoveConnectedDoctor_WithNonExistentId_ThrowsException()
        {
            var patient = CreatePatient();

            Assert.ThrowsAny<Exception>(() => patient.RemoveConnectedDoctor(Guid.NewGuid()));
        }

        [Fact]
        public void RemoveConnectedDoctor_UpdatesTimestamp()
        {
            var patient = CreatePatient();
            var doctorId = Guid.NewGuid();
            patient.AddConnectedDoctor(doctorId);
            var before = patient.UpdatedAt;
            System.Threading.Thread.Sleep(10);

            patient.RemoveConnectedDoctor(doctorId);

            Assert.True(patient.UpdatedAt > before);
        }

        // ── AssignGuardian / RemoveGuardian ──────────────────────────────────

        [Fact]
        public void AssignGuardian_SetsGuardianId()
        {
            var patient = CreatePatient();
            var guardianId = Guid.NewGuid();

            patient.AssignGuardian(guardianId);

            Assert.Equal(guardianId, patient.GuardianId);
        }

        [Fact]
        public void RemoveGuardian_SetsGuardianIdToNull()
        {
            var patient = CreatePatient();
            patient.AssignGuardian(Guid.NewGuid());

            patient.RemoveGuardian();

            Assert.Null(patient.GuardianId);
        }

        // ── UploadTestResult ─────────────────────────────────────────────────

        [Fact]
        public void UploadTestResult_ByPatient_AddsToCollection()
        {
            var patient = CreatePatient();
            var result = new TestResult(
                patient.UserId, "Blood Test", "Normal", DateTime.UtcNow,
                "/files/test.pdf", HealLink.Domain.Enums.FileType.PDF);

            patient.UploadTestResult(result, patient.UserId);

            Assert.Single(patient.TestResults);
        }

        [Fact]
        public void UploadTestResult_ByGuardian_AddsToCollection()
        {
            var patient = CreatePatient();
            var guardianId = Guid.NewGuid();
            patient.AssignGuardian(guardianId);
            var result = new TestResult(
                patient.UserId, "Blood Test", "Normal", DateTime.UtcNow,
                "/files/test.pdf", HealLink.Domain.Enums.FileType.PDF);

            patient.UploadTestResult(result, guardianId);

            Assert.Single(patient.TestResults);
        }

        [Fact]
        public void UploadTestResult_ByUnauthorizedUser_ThrowsUnauthorizedAccessException()
        {
            var patient = CreatePatient();
            var result = new TestResult(
                patient.UserId, "Blood Test", "Normal", DateTime.UtcNow,
                "/files/test.pdf", HealLink.Domain.Enums.FileType.PDF);

            Assert.Throws<UnauthorizedAccessException>(() =>
                patient.UploadTestResult(result, Guid.NewGuid()));
        }
    }
}
