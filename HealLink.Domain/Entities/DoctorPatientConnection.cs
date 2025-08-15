using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class DoctorPatientConnection : Entity
    {
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public ConnectionStatus Status { get; private set; }
        public DateTime? AcceptedAt { get; private set; }

        private DoctorPatientConnection() { } // For EF

        public DoctorPatientConnection(Guid doctorId, Guid patientId)
        {
            DoctorId = doctorId;
            PatientId = patientId;
            Status = ConnectionStatus.Pending;
        }

        public void Accept()
        {
            Status = ConnectionStatus.Accepted;
            AcceptedAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public void Reject()
        {
            Status = ConnectionStatus.Rejected;
            UpdateTimestamp();
        }
    }
} 