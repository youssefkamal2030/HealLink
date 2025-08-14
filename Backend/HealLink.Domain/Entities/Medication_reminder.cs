using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class MedicationReminder : Entity
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
