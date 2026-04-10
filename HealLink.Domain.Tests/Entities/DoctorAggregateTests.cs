using System;
using System.Collections.Generic;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;
using Xunit;

namespace HealLink.Domain.Tests.Entities
{
    // TODO: [TEST-NEXT] Add tests for AddNotification() — verify notification is added to the collection and null throws ArgumentNullException.
    // TODO: [TEST-NEXT] Add tests for UpdateProfessionalDetails() — verify Specialization and CurrentWorkplace are updated and timestamp changes.
    // TODO: [TEST-NEXT] Add tests for RefreshQRCodeIfNeeded() — verify it generates a new QR code when none exists, and does not regenerate when the existing one is still valid.
    // TODO: [TEST-NEXT] Add a test for AddConnection_AfterRejectedConnection_AllowsNewRequest — a patient whose connection was rejected should be able to send a new request (Status == Rejected should not block AddConnection).
    public class DoctorAggregateTests
    {
        private Doctor CreateDoctor() => new Doctor(Guid.NewGuid());

        private DoctorPatientConnection AddPendingConnection(Doctor doctor)
        {
            var connection = new DoctorPatientConnection(doctor.Id, Guid.NewGuid());
            doctor.AddConnection(connection);
            return connection;
        }

        // ── AddConnection ────────────────────────────────────────────────────

        [Fact]
        public void AddConnection_WithNullConnection_ThrowsArgumentNullException()
        {
            var doctor = CreateDoctor();
            Assert.Throws<ArgumentNullException>(() => doctor.AddConnection(null));
        }

        [Fact]
        public void AddConnection_WithNewPatient_AddsToCollection()
        {
            var doctor = CreateDoctor();
            var connection = new DoctorPatientConnection(doctor.Id, Guid.NewGuid());

            doctor.AddConnection(connection);

            Assert.Single(doctor.PatientConnections);
        }

        [Fact]
        public void AddConnection_WithDuplicatePendingPatient_ThrowsInvalidOperationException()
        {
            var doctor = CreateDoctor();
            var patientId = Guid.NewGuid();
            var first = new DoctorPatientConnection(doctor.Id, patientId);
            var duplicate = new DoctorPatientConnection(doctor.Id, patientId);

            doctor.AddConnection(first);

            var ex = Assert.Throws<InvalidOperationException>(() => doctor.AddConnection(duplicate));
            Assert.Contains("already exists", ex.Message);
        }

        // ── AcceptPatientRequest ─────────────────────────────────────────────

        [Fact]
        public void AcceptPatientRequest_WithValidPendingConnection_SetsStatusAccepted()
        {
            var doctor = CreateDoctor();
            var connection = AddPendingConnection(doctor);

            doctor.AcceptPatientRequest(connection.Id);

            Assert.Equal(ConnectionStatus.Accepted, connection.Status);
        }

        [Fact]
        public void AcceptPatientRequest_RaisesConnectionAcceptedEvent()
        {
            var doctor = CreateDoctor();
            var connection = AddPendingConnection(doctor);

            doctor.AcceptPatientRequest(connection.Id);

            Assert.Single(doctor.DomainEvents);
            var evt = Assert.IsType<ConnectionAcceptedEvent>(doctor.DomainEvents.First());
            Assert.Equal(connection.Id, evt.ConnectionId);
            Assert.Equal(doctor.Id, evt.DoctorId);
            Assert.Equal(connection.PatientId, evt.PatientId);
        }

        [Fact]
        public void AcceptPatientRequest_WithNonExistentConnection_ThrowsInvalidOperationException()
        {
            var doctor = CreateDoctor();

            Assert.Throws<InvalidOperationException>(() => doctor.AcceptPatientRequest(Guid.NewGuid()));
        }

        [Fact]
        public void AcceptPatientRequest_WithAlreadyAcceptedConnection_ThrowsInvalidOperationException()
        {
            var doctor = CreateDoctor();
            var connection = AddPendingConnection(doctor);
            doctor.AcceptPatientRequest(connection.Id);
            doctor.ClearDomainEvents();

            Assert.Throws<InvalidOperationException>(() => doctor.AcceptPatientRequest(connection.Id));
        }

        // ── RejectPatientRequest ─────────────────────────────────────────────

        [Fact]
        public void RejectPatientRequest_WithValidPendingConnection_RemovesFromCollection()
        {
            var doctor = CreateDoctor();
            var connection = AddPendingConnection(doctor);

            doctor.RejectPatientRequest(connection.Id);

            Assert.Empty(doctor.PatientConnections);
        }

        [Fact]
        public void RejectPatientRequest_RaisesConnectionRejectedEvent()
        {
            var doctor = CreateDoctor();
            var connection = AddPendingConnection(doctor);

            doctor.RejectPatientRequest(connection.Id);

            Assert.Single(doctor.DomainEvents);
            var evt = Assert.IsType<ConnectionRejectedEvent>(doctor.DomainEvents.First());
            Assert.Equal(connection.Id, evt.ConnectionId);
            Assert.Equal(doctor.Id, evt.DoctorId);
        }

        [Fact]
        public void RejectPatientRequest_WithNonExistentConnection_ThrowsInvalidOperationException()
        {
            var doctor = CreateDoctor();

            Assert.Throws<InvalidOperationException>(() => doctor.RejectPatientRequest(Guid.NewGuid()));
        }

        // ── Approve ──────────────────────────────────────────────────────────

        [Fact]
        public void Approve_SetsIsApprovedTrue()
        {
            var doctor = CreateDoctor();

            doctor.Approve();

            Assert.True(doctor.IsApproved);
        }

        // ── SetChatAvailability ──────────────────────────────────────────────

        [Fact]
        public void SetChatAvailability_ToTrue_SetsIsAvailableForChatTrue()
        {
            var doctor = CreateDoctor();

            doctor.SetChatAvailability(true);

            Assert.True(doctor.IsAvailableForChat);
        }

        [Fact]
        public void SetChatAvailability_ToFalse_SetsIsAvailableForChatFalse()
        {
            var doctor = CreateDoctor();
            doctor.SetChatAvailability(true);

            doctor.SetChatAvailability(false);

            Assert.False(doctor.IsAvailableForChat);
        }

        // ── UpdatePersonalInfo / UpdateAddress ───────────────────────────────

        [Fact]
        public void UpdatePersonalInfo_SetsPersonalInfo()
        {
            var doctor = CreateDoctor();
            var info = new PersonalInfo("Dr. Ahmed", "Male", "Egyptian");

            doctor.UpdatePersonalInfo(info);

            Assert.Equal(info, doctor.PersonalInfo);
        }

        [Fact]
        public void UpdateAddress_SetsAddress()
        {
            var doctor = CreateDoctor();
            var address = new Address("Cairo", "Egypt");

            doctor.UpdateAddress(address);

            Assert.Equal(address, doctor.Address);
        }

        // ── GenerateQRCode / IsQRCodeValid ───────────────────────────────────

        [Fact]
        public void GenerateQRCode_SetsQRCodeAndGeneratedAt()
        {
            var doctor = CreateDoctor();

            doctor.GenerateQRCode();

            Assert.NotNull(doctor.QRCode);
            Assert.NotNull(doctor.QRCode.GeneratedAt);
        }

        [Fact]
        public void IsQRCodeValid_WhenJustGenerated_ReturnsTrue()
        {
            var doctor = CreateDoctor();
            doctor.GenerateQRCode();

            Assert.True(doctor.IsQRCodeValid());
        }

        [Fact]
        public void IsQRCodeValid_WhenNoQRCodeGenerated_ReturnsFalse()
        {
            var doctor = CreateDoctor();

            Assert.False(doctor.IsQRCodeValid());
        }
    }
}
