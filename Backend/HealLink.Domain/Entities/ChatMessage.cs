using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class ChatMessage : Entity
    {
        public Guid SenderId { get; private set; }
        public Guid ReceiverId { get; private set; }
        public string Content { get; private set; }
        public MessageStatus Status { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? ReadAt { get; private set; }
      

        private ChatMessage() { } // For EF

        public ChatMessage(Guid senderId, Guid receiverId, string content, Guid? guardianId = null)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Status = MessageStatus.Sent;
        }

        public void MarkAsDelivered()
        {
            Status = MessageStatus.Delivered;
            DeliveredAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public void MarkAsRead()
        {
            Status = MessageStatus.Read;
            ReadAt = DateTime.UtcNow;
            UpdateTimestamp();
        }
    }
}
