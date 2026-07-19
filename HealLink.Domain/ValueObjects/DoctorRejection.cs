using System;

namespace HealLink.Domain.ValueObjects
{
    public class DoctorRejection
    {
        public string Reason { get; private set; }
        public Guid RejectedBy { get; private set; }
        public DateTime RejectedAt { get; private set; }

        public DoctorRejection(string reason, Guid rejectedBy, DateTime rejectedAt)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("Rejection reason cannot be empty.", nameof(reason));
            if (rejectedBy == Guid.Empty)
                throw new ArgumentException("RejectedBy must be a valid admin ID.", nameof(rejectedBy));

            Reason = reason;
            RejectedBy = rejectedBy;
            RejectedAt = rejectedAt;
        }

        private DoctorRejection() { } // EF Core
    }
}
