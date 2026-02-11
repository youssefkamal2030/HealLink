using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealLink.Infrastructure.Repositories
{
    public class ChatRepository(HealLinkDbContext context, ILogger<ChatRepository> logger ) : IChatRepository
    { 
        private readonly HealLinkDbContext _context = context;
        private readonly ILogger<ChatRepository> _logger = logger;
        public async Task AddChatMessageAsync(ChatMessage message)
        {
            if (message is null)
            {
                _logger.LogWarning("AddChatMessageAsync was called with a null message.");
                throw new ArgumentNullException(nameof(message));
            }

            if (string.IsNullOrWhiteSpace(message.Content))
            {
                _logger.LogWarning("Attempt to store a chat message with empty content. SenderId: {SenderId}",
                    message.SenderId);
                throw new ArgumentException("Message content cannot be empty.", nameof(message));
            }

            if (message.SenderId == Guid.Empty)
            {
                _logger.LogWarning("Attempt to store a chat message with invalid SenderId.");
                throw new ArgumentException("SenderId is required.", nameof(message));
            }

            if (message.CreatedAt == default)
            {
                message.SetCreatedAt(DateTime.UtcNow);
               
            }

            try
            {
                await _context.ChatMessages.AddAsync(message);
                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Chat message stored successfully. MessageId: {MessageId}, SenderId: {SenderId}",
                    message.Id,
                    message.SenderId);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex,
                    "Database error while storing chat message. SenderId: {SenderId}",
                    message.SenderId);

                throw; // Let upper layer decide how to handle it
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Unexpected error while storing chat message. SenderId: {SenderId}",
                    message.SenderId);

                throw;
            }
        }


        public Task<List<ChatMessage>> GetChatHistoryAsync(string userId1, string userId2)
        {
            throw new NotImplementedException();
        }
    }
}
