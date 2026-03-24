using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] Notification.Type is a raw string — it should be a strongly-typed enum or value object to prevent invalid notification types.
    // TODO: [DDD] Notification has both DoctorId and PatientId as nullable fields — this is a design smell; consider a polymorphic recipient value object or separate notification types.
    // TODO: [DDD] Doctor and Patient navigation properties are public with no setter — EF navigation properties should be private set to prevent external assignment.
    // TODO: [AGGREGATE-MISSING] Notification has no aggregate boundary. Notifications are currently attached to Doctor via a public collection with a public setter, completely outside any aggregate's control. Two options:
    //   Option A (preferred): Fold notifications into their respective aggregates — DoctorAggregate owns doctor notifications, PatientAggregate owns patient notifications. Each aggregate exposes an AddNotification() method and raises the appropriate domain event.
    //   Option B: Create a dedicated NotificationAggregate per recipient, keyed by RecipientId + RecipientType, owning a List<Notification>. This is appropriate if notification querying/pagination becomes a first-class concern.
    //   Either way, the current pattern of Doctor.notifications having a public setter must be removed.
    public class Notification : Entity
    {
        // Recipient information
        public Guid? DoctorId { get; private set; }
        public Guid? PatientId { get; private set; }
        public RecipientType RecipientType { get; private set; }
        
        // Notification content
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Type { get; private set; }
        
        // Status
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }
        
        // Navigation properties
        public Doctor Doctor { get; private set; }
        public Patient Patient { get; private set; }
        
        private Notification() { } // EF Core
        
        // Factory method for doctor notifications
        public static Notification ForDoctor(
            Guid doctorId,
            string title,
            string message,
            string type)
        {
            return new Notification
            {
                DoctorId = doctorId,
                RecipientType = RecipientType.Doctor,
                Title = title ?? throw new ArgumentNullException(nameof(title)),
                Message = message ?? throw new ArgumentNullException(nameof(message)),
                Type = type ?? throw new ArgumentNullException(nameof(type)),
                IsRead = false
            };
        }
        
        // Factory method for patient notifications
        public static Notification ForPatient(
            Guid patientId,
            string title,
            string message,
            string type)
        {
            return new Notification
            {
                PatientId = patientId,
                RecipientType = RecipientType.Patient,
                Title = title ?? throw new ArgumentNullException(nameof(title)),
                Message = message ?? throw new ArgumentNullException(nameof(message)),
                Type = type ?? throw new ArgumentNullException(nameof(type)),
                IsRead = false
            };
        }
        
        public void MarkAsRead()
        {
            IsRead = true;
            ReadAt = DateTime.UtcNow;
            UpdateTimestamp();
        }
    }
}
