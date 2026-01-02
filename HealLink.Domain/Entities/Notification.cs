using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
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
