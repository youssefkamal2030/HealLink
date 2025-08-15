using System;
using System.Collections.Generic;
using HealLink.Domain.Base;

namespace HealLink.Domain.Entities
{
    public class Notification : Entity
    {
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }
        public string Type { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime? ReadAt { get; private set; }
        public Dictionary<string, object> Data { get; private set; }

        private Notification() { } 

        public Notification(Guid userId, string title, string message, string type, Dictionary<string, object> data = null)
        {
            UserId = userId;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Type = type ?? throw new ArgumentNullException(nameof(type));
            IsRead = false;
            Data = data ?? new Dictionary<string, object>();
        }

        public void MarkAsRead()
        {
            IsRead = true;
            ReadAt = DateTime.UtcNow;
            UpdateTimestamp();
        }
    }
}
