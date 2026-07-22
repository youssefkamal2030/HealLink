using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class Notification : AggregateRoot
    {
        public Guid RecipientId { get; private set; }
        public RecipientType RecipientType { get; private set; }

        public string Title { get; private set; }
        public string Message { get; private set; }
        public NotificationType Type { get; private set; }

        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }

        private Notification() { } // EF Core

        public Notification(Guid recipientId, RecipientType recipientType, string title, string message, NotificationType type)
        {
            if (recipientId == Guid.Empty)
                throw new ArgumentException("RecipientId cannot be empty.", nameof(recipientId));

            RecipientId = recipientId;
            RecipientType = recipientType;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type;
            IsRead = false;
        }

        public void MarkAsRead()
        {
            IsRead = true;
            ReadAt = DateTime.UtcNow;
            UpdateTimestamp();
        }
    }
}
