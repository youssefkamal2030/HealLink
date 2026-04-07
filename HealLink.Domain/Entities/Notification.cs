using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] Notification has both DoctorId and PatientId as nullable fields — this is a design smell; consider a polymorphic recipient value object or separate notification types.
    // TODO: [AGGREGATE-MISSING] Notification has no aggregate boundary. Notifications are currently created directly by NotificationPersistenceService outside any aggregate. If notification querying/pagination becomes a first-class concern, consider a dedicated NotificationAggregate per recipient keyed by RecipientId + RecipientType.
    //
    // ── DESIGN ISSUE: Dual nullable recipient IDs ────────────────────────────────────────────────
    //
    // The current schema stores two nullable FK columns — DoctorId and PatientId — on every row.
    // At any given time exactly one is populated and the other is NULL. RecipientType tells you which.
    //
    // Problems with this approach:
    //   1. Two nullable FK columns where only one is ever used — the DB schema can't enforce "exactly one recipient".
    //   2. Nothing in the C# type system prevents accidentally setting both IDs, or neither.
    //      The factory methods are the only guard, and they can be bypassed via the EF private constructor.
    //   3. Queries must always branch on RecipientType — no single generic "get notifications for X" query.
    //   4. Adding a third recipient type (e.g. Guardian) requires a third nullable FK column — the schema
    //      grows linearly with recipient types.
    //
    // ── ALTERNATIVE A: Single RecipientId + RecipientType ────────────────────────────────────────
    //
    // Replace DoctorId and PatientId with a single non-nullable RecipientId (Guid) and keep RecipientType.
    //
    //   public Guid RecipientId { get; private set; }
    //   public RecipientType RecipientType { get; private set; }
    //
    // Benefits:
    //   - One non-nullable column — the DB schema enforces "exactly one recipient".
    //   - A single index on (RecipientId, RecipientType) covers all notification queries.
    //   - Adding Guardian support requires only a new RecipientType enum value and a ForGuardian() factory.
    //
    // Tradeoff:
    //   - Loses the direct EF navigation properties (Doctor, Patient) since there's no typed FK.
    //   - Requires a migration: drop DoctorId and PatientId columns, add RecipientId column.
    //   - INotificationRepository queries must be updated to filter on (RecipientId, RecipientType).
    //
    // ── ALTERNATIVE B: Abstract base + concrete subtypes (polymorphic hierarchy) ─────────────────
    //
    // Introduce an abstract Notification base class and concrete subtypes per recipient type.
    // The system then understands "a notification" as a general concept, not just the specific cases.
    //
    //   public abstract class Notification : Entity
    //   {
    //       public string Title { get; private set; }
    //       public string Message { get; private set; }
    //       public NotificationType Type { get; private set; }
    //       public bool IsRead { get; private set; }
    //       public void MarkAsRead() { ... }
    //   }
    //
    //   public class DoctorNotification : Notification
    //   {
    //       public Guid DoctorId { get; private set; }
    //       public Doctor? Doctor { get; private set; }
    //   }
    //
    //   public class PatientNotification : Notification
    //   {
    //       public Guid PatientId { get; private set; }
    //       public Patient? Patient { get; private set; }
    //   }
    //
    // Benefits:
    //   - Fully type-safe — the C# type system enforces "a DoctorNotification always has a DoctorId".
    //   - No nullable columns, no RecipientType discriminator needed in application code.
    //   - EF navigation properties work naturally on each subtype.
    //   - Adding GuardianNotification is a new class — no changes to existing types.
    //   - Factory methods become constructors on the concrete types — cleaner and more discoverable.
    //   - The repository can return IEnumerable<Notification> for general queries and
    //     IEnumerable<DoctorNotification> for doctor-specific ones — the type carries the intent.
    //
    // EF Core mapping strategy options:
    //   - TPH (Table-per-hierarchy): one Notifications table with a discriminator column.
    //     Nullable columns remain but are semantically correct — EF manages them automatically.
    //     Migration cost: minimal — add discriminator column, existing data maps cleanly.
    //     RECOMMENDED for this project.
    //   - TPT (Table-per-type): base Notifications table + DoctorNotifications + PatientNotifications.
    //     Fully normalized, no nullable columns. Migration cost: moderate — split existing table.
    //   - TPC (Table-per-concrete-type): separate tables per subtype, no shared base table.
    //     Best query performance, no joins. Migration cost: highest — existing data must be split.
    //
    // Tradeoff vs Alternative A:
    //   - More classes and more EF configuration than Alternative A.
    //   - TPH still has nullable columns in the DB (just managed by EF, not by hand).
    //   - Overkill if the only recipient types will ever be Doctor and Patient.
    //     Worth it if Guardian notifications or other recipient types are planned.
    //
    // ── RECOMMENDATION ───────────────────────────────────────────────────────────────────────────
    //
    // If Guardian notifications are planned: go with Alternative B (TPH). The type hierarchy
    // scales cleanly, the migration is small, and the domain model becomes self-documenting.
    //
    // If only Doctor and Patient will ever receive notifications: Alternative A is simpler and
    // sufficient — one column, one index, minimal migration.
    //
    // Either way, do NOT refactor this in isolation. Bundle it with the next notification feature
    // (Guardian notifications, pagination, or read/unread counts) to justify the migration cost.
    public class Notification : Entity
    {
        // Recipient information
        public Guid? DoctorId { get; private set; }
        public Guid? PatientId { get; private set; }
        public RecipientType RecipientType { get; private set; }
        
        // Notification content
        public string Title { get; private set; }
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }
        
        // Status
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }
        
        // Navigation properties
        public Doctor Doctor { get; private set; }
        public Patient Patient { get; private set; }
        
        private Notification() { } // EF Core
        
        // ── Factory methods ──────────────────────────────────────────────────────────────────────
        // These exist to enforce that a Notification is always created in a valid state for a
        // specific recipient type. Without them, callers would have to set 5+ properties manually
        // and could forget RecipientType, set both IDs, or leave Title null.
        //
        // Each factory encodes one invariant: "a doctor notification has DoctorId set, PatientId null,
        // RecipientType = Doctor". That rule lives here once instead of being repeated at every call site.
        //
        // Under Alternative A (single RecipientId): factory signatures stay the same, only internal
        // assignments change — ForDoctor sets RecipientId = doctorId, RecipientType = Doctor.
        //
        // Under Alternative B (abstract hierarchy): these factories are replaced by constructors on
        // DoctorNotification and PatientNotification. The abstract base has no factory methods —
        // you construct the concrete type directly: new DoctorNotification(doctorId, title, ...).
        // This is cleaner because the type itself communicates intent at the call site.
        
        // Factory method for doctor notifications
        public static Notification ForDoctor(
            Guid doctorId,
            string title,
            string message,
            NotificationType type)
        {
            return new Notification
            {
                DoctorId = doctorId,
                RecipientType = RecipientType.Doctor,
                Title = title ?? throw new ArgumentNullException(nameof(title)),
                Message = message ?? throw new ArgumentNullException(nameof(message)),
                Type = type ,
                IsRead = false
            };
        }
        
        // Factory method for patient notifications
        public static Notification ForPatient(
            Guid patientId,
            string title,
            string message,
            NotificationType type)
        {
            return new Notification
            {
                PatientId = patientId,
                RecipientType = RecipientType.Patient,
                Title = title ?? throw new ArgumentNullException(nameof(title)),
                Message = message ?? throw new ArgumentNullException(nameof(message)),
                Type = type ,
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
