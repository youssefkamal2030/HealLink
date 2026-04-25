using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class DoctorPatientConnection : AggergateRoot
    {
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public ConnectionStatus Status { get; private set; }
        public DateTime? AcceptedAt { get; private set; }
        public Doctor? Doctor { get; private set; }
        public Patient? Patient { get; private set; }
        private DoctorPatientConnection() { } // For EF

        private DoctorPatientConnection(Guid doctorId, Guid patientId)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            Status = ConnectionStatus.Pending;
        }

        /// <summary>
        /// Factory method for creating a new connection request from a patient to a doctor.
        /// </summary>
        public static DoctorPatientConnection Request(Guid doctorId, Guid patientId)
        {
            if (doctorId == Guid.Empty) throw new ArgumentException("DoctorId cannot be empty.", nameof(doctorId));
            if (patientId == Guid.Empty) throw new ArgumentException("PatientId cannot be empty.", nameof(patientId));

            return new DoctorPatientConnection(doctorId, patientId);
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

        internal void Terminate()
        {
            if (Status != ConnectionStatus.Accepted)
                throw new InvalidOperationException("Only accepted connections can be terminated.");
            
            Status = ConnectionStatus.Terminated;
            UpdateTimestamp();
        }

    }
} 