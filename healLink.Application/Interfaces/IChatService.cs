using healLink.Application.Common.Models;
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
        Task<Result<List<ChatMessage>>> GetChatHistoryAsync(Guid userId1, Guid userId2);
        Task<Result<Guid>> SendMessageAsync(Guid senderId, Guid receiverId, string message);
        Task<Result<bool>> ValidateConnection (Guid doctorId, Guid patientId);
    }
}
