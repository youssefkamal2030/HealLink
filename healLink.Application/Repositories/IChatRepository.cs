using HealLink.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Repositories
{
    public interface IChatRepository
    {
        Task<List<ChatMessage>> GetChatHistoryAsync(string userId1, string userId2);
        Task AddChatMessageAsync(ChatMessage message);
    }
}
