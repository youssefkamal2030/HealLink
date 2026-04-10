using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [REFACTOR-P3] DoctorPatientConnection.Accept() and Reject() are called directly from outside the aggregate — these should only be callable through DoctorAggregate.AcceptPatientRequest() and RejectPatientRequest(), which already exist and enforce the Pending status guard. The public Accept()/Reject() methods on the entity are a leaky API; consider making them internal.
    public class DoctorPatientConnection : Entity
    {
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public ConnectionStatus Status { get; private set; }
        public DateTime? AcceptedAt { get; private set; }
        public Doctor? Doctor { get; private set; }
        public Patient? Patient { get; private set; }
        private DoctorPatientConnection() { } // For EF

        public DoctorPatientConnection(Guid doctorId, Guid patientId)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            Status = ConnectionStatus.Pending;
        }

        internal void Accept()
        {
            Status = ConnectionStatus.Accepted;
            AcceptedAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        internal void Reject()
        {
            Status = ConnectionStatus.Rejected;
            UpdateTimestamp();
        }

    }
} 