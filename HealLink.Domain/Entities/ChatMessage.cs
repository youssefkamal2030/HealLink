using System;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;

namespace HealLink.Domain.Entities
{
    public class ChatMessage : AggregateRoot
    {
        public Guid SenderId { get; private set; }
        public Guid ReceiverId { get; private set; }
        public string Content { get; private set; }
        public MessageStatus Status { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? ReadAt { get; private set; }
        public bool IsDeleted { get; private set; } = false;
      

        private ChatMessage() { } // For EF

        private ChatMessage(Guid senderId, Guid receiverId, string content)
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

        /// <summary>
        /// Factory method for sending a new chat message.
        /// </summary>
        public static ChatMessage Send(Guid senderId, Guid receiverId, string content)
            => new ChatMessage(senderId, receiverId, content);

        public void MarkAsDelivered()
        {
            if (Status != MessageStatus.Sent)
                throw new InvalidOperationException("Can only mark as Delivered from Sent state.");
            Status = MessageStatus.Delivered;
            DeliveredAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public void MarkAsRead()
        {
            if (Status != MessageStatus.Delivered)
                throw new InvalidOperationException("Can only mark as Read from Delivered state.");
            Status = MessageStatus.Read;
            ReadAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        // [PENDING] TODO: [REFACTOR-AUTH] Remove authorization logic from domain entity after centralized-authorization-infrastructure is complete
        // PROBLEM: Domain entity is handling authorization (checking requestingUserId == SenderId in SoftDelete)
        //          This violates Clean Architecture - domain should only contain business rules
        // SPEC: .kiro/specs/centralized-authorization-infrastructure (In Progress)
        // MIGRATION: After spec implementation:
        //   1. Remove requestingUserId parameter from SoftDelete()
        //   2. Remove UnauthorizedAccessException throws
        //   3. EditContent() already fixed - no auth logic
        //   4. Add [Authorize(AuthorizationPolicies.ResourceOwner)] to DeleteMessageCommand
        
        public void EditContent(string newContent)
        {
          

            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Message content cannot be empty", nameof(newContent));

            if (newContent.Length > 5000)
                throw new ArgumentException("Message content cannot exceed 5000 characters", nameof(newContent));

            Content = newContent;
            UpdateTimestamp();
        }

        public void SoftDelete(Guid requestingUserId)
        {
            if (requestingUserId != SenderId)
                throw new UnauthorizedAccessException("Only the sender can delete the message.");

            IsDeleted = true;
            UpdateTimestamp();
        }
      
    }
}
