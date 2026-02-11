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

        public Task<List<ChatMessage>> GetChatHistoryAsync(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }

        public Task SendMessageAsync(Guid senderId, Guid receiverId, string message)
        {
            
            var chatMessage = new ChatMessage(senderId, receiverId, message);
            return _chatRepository.AddChatMessageAsync(chatMessage);
        }
    }
}
