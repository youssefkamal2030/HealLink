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

        public ChatMessage(Guid senderId, Guid receiverId, string content)
        {
            if (senderId == Guid.Empty)
                throw new ArgumentException("SenderId cannot be empty", nameof(senderId));
            
            if (receiverId == Guid.Empty)
                throw new ArgumentException("ReceiverId cannot be empty", nameof(receiverId));
            
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Message content cannot be empty", nameof(content));
            
            if (content.Length > 5000)
                throw new ArgumentException("Message content cannot exceed 5000 characters", nameof(content));

            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
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
        public void SetCreatedAt(DateTime createdAt)
        {
            if (createdAt == default)
                throw new ArgumentException("CreatedAt must be a valid date.", nameof(createdAt));
            CreatedAt = createdAt;
        }
    }
}
