using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [AGGREGATE] MedicationReminder belongs inside PrescriptionAggregate as an owned entity — per BR-REM-01, reminders are generated from a prescription's medication schedule. The aggregate should create reminders when a MedicationDosage is added and own their lifecycle.
    // TODO: [AGGREGATE] MarkAsTaken() and MarkAsMissed() should only be callable through PatientAggregate (which enforces the guardian authorization check per BR-REM-06) — direct calls on the entity from outside the aggregate bypass that invariant.
    // TODO: [AGGREGATE] MarkAsMissed() should raise a MedicationMissedEvent — the event already exists in the domain (MedicationMissedEvent.cs) but is never raised. The owning aggregate (PrescriptionAggregate or PatientAggregate) must raise it so the guardian notification (BR-REM-05) can be dispatched.
    public class MedicationReminder : AggergateRoot
    {
        public Guid PatientId { get; private set; }
        public Guid PrescriptionId { get; private set; }
        public string MedicationName { get; private set; }
        public DateTime ScheduledTime { get; private set; }
        public MedicationReminderStatus Status { get; private set; }
        public DateTime? TakenAt { get; private set; }
        public DateTime? SnoozedUntil { get; private set; }
        public int SnoozeCount { get; private set; }

        private MedicationReminder() { } // For EF

        public MedicationReminder(Guid patientId, Guid prescriptionId, string medicationName, DateTime scheduledTime)
        {
            PatientId = patientId;
            PrescriptionId = prescriptionId;
            MedicationName = medicationName ?? throw new ArgumentNullException(nameof(medicationName));
            ScheduledTime = scheduledTime;
            Status = MedicationReminderStatus.Pending;
            SnoozeCount = 0;
        }

        public void MarkAsTaken()
        {
            Status = MedicationReminderStatus.Taken;
            TakenAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public void Snooze(int snoozeMinutes = 15)
        {
            Status = MedicationReminderStatus.Snoozed;
            SnoozedUntil = DateTime.UtcNow.AddMinutes(snoozeMinutes);
            SnoozeCount++;
            UpdateTimestamp();
        }

        public void MarkAsMissed()
        {
            Status = MedicationReminderStatus.Missed;
            UpdateTimestamp();
        }

        public bool IsDue()
        {
            return Status == MedicationReminderStatus.Pending && DateTime.UtcNow >= ScheduledTime;
        }

        public bool IsSnoozeExpired()
        {
            return Status == MedicationReminderStatus.Snoozed &&
                   SnoozedUntil.HasValue &&
                   DateTime.UtcNow >= SnoozedUntil.Value;
        }
    }
}
