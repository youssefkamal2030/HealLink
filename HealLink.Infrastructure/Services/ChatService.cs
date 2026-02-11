using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Services
{
    public class ChatService(IChatRepository chatRepository) : IChatService
    {
        private readonly IChatRepository _chatRepository = chatRepository;

        public async Task<List<ChatMessage>> GetChatHistoryAsync(Guid userId1, Guid userId2)
        {
            return await _chatRepository.GetChatHistoryAsync(userId1, userId2);
        }

        public async Task SendMessageAsync(Guid senderId, Guid receiverId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("Message content cannot be empty", nameof(message));
            }

            if (senderId == Guid.Empty)
            {
                throw new ArgumentException("SenderId is required", nameof(senderId));
            }

            if (receiverId == Guid.Empty)
            {
                throw new ArgumentException("ReceiverId is required", nameof(receiverId));
            }

            var chatMessage = new ChatMessage(senderId, receiverId, message);
            await _chatRepository.AddChatMessageAsync(chatMessage);
        }
    }
}
