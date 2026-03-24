using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    // TODO: [DDD] ChatMessage.SetCreatedAt() allows external mutation of CreatedAt — this is an infrastructure concern and breaks the immutability of audit timestamps.
    // TODO: [DDD] ChatMessage does not raise domain events on MarkAsRead() or MarkAsDelivered() — these transitions may need to notify other parts of the system.
    // TODO: [AGGREGATE-MISSING] ChatMessage has no aggregate. ChatMessage entities need a ConversationAggregate as their root, keyed by the two participant IDs (SenderId + ReceiverId pair). Without it:
    //   - BR-CHAT-01 (chat only between connected users) cannot be enforced at the domain level — nothing prevents a message between unconnected users.
    //   - BR-CHAT-02 (doctor must have IsAvailableForChat = true) has no enforcement point.
    //   - BR-CHAT-04 (Sent → Delivered → Read is one-way) is defined on the entity but the aggregate is the right place to guard the transition sequence across the conversation.
    //   ConversationAggregate should own a List<ChatMessage>, enforce the connection pre-condition on creation, and raise MessageSentEvent / MessageReadEvent domain events.
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
