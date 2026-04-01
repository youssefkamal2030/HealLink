using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using MediatR;

namespace HealLink.Domain.Entities
{
    // TODO: [AGGREGATE] DoctorPatientConnection imports MediatR — the domain layer should have no dependency on application/infrastructure frameworks.
    // TODO: [DDD] No domain event raised on Accept() or Reject() — these are significant state transitions; events should be raised here or by the owning aggregate.
    // TODO: [AGGREGATE] DoctorPatientConnection is owned by DoctorAggregate — it must only be created and mutated through DoctorAggregate methods (AddConnection, AcceptPatientRequest, RejectPatientRequest). Direct instantiation from the application layer bypasses the duplicate-connection invariant (BR-CON-02) enforced in DoctorAggregate.AddConnection().
    // TODO: [AGGREGATE] PatientAggregate also holds a List<DoctorPatientConnection> — this is the dual-ownership problem. Pick one: DoctorAggregate owns the connection objects; PatientAggregate holds only a List<Guid> of connected DoctorIds updated via domain events (ConnectionAcceptedEvent / ConnectionRejectedEvent).
    // TODO: [DOMAIN-NEXT] Remove the `using MediatR;` import — DoctorPatientConnection has no reason to reference MediatR. It's a leftover from before the domain event refactor.
    // TODO: [DOMAIN-NEXT] Make Doctor and Patient navigation properties private set — `public Doctor Doctor { get; private set; }` is correct but `Patient Patient` should also be `private set`. Both should be nullable (Doctor? / Patient?) since they're loaded by EF on demand, not always present.
    public class DoctorPatientConnection : Entity
    {
        public Guid DoctorId { get; private set; }
        public Guid PatientId { get; private set; }
        public ConnectionStatus Status { get; private set; }
        public DateTime? AcceptedAt { get; private set; }
        public Doctor Doctor { get; private set; }
        public Patient Patient { get; private set; }
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