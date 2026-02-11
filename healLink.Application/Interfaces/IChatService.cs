using HealLink.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatMessage>> GetChatHistoryAsync(Guid userId1, Guid userId2);
        Task SendMessageAsync(Guid senderId, Guid receiverId, string message);
    }
}
