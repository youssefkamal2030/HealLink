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

                _logger.LogInformation(
                    "Chat message staged for save. MessageId: {MessageId}, SenderId: {SenderId}",
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


        public async Task<List<ChatMessage>> GetChatHistoryAsync(Guid userId1, Guid userId2)
        {
            try
            {
                var messages = await _context.ChatMessages
                    .Where(m => 
                        (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                        (m.SenderId == userId2 && m.ReceiverId == userId1))
                    .OrderBy(m => m.CreatedAt)
                    .ToListAsync();

                _logger.LogInformation(
                    "Retrieved {Count} messages between users {UserId1} and {UserId2}",
                    messages.Count,
                    userId1,
                    userId2);

                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error retrieving chat history between users {UserId1} and {UserId2}",
                    userId1,
                    userId2);
                throw;
            }
        }
    }
}
