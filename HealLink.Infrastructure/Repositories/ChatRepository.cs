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
            await _context.ChatMessages.AddAsync(message);
        }

        public async Task<ChatMessage?> GetByIdAsync(Guid messageId, CancellationToken cancellationToken = default)
            => await _context.ChatMessages.FindAsync(new object[] { messageId }, cancellationToken);

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

        public async Task UpdateAsync(ChatMessage message, CancellationToken cancellationToken = default)
        {
            _context.ChatMessages.Update(message);
        }

        public async Task<List<ChatMessage>> SearchMessagesAsync(Guid userId1, Guid userId2, string searchTerm, CancellationToken cancellationToken = default)
        {
            try
            {
                var messages = await _context.ChatMessages
                    .Where(m => 
                        ((m.SenderId == userId1 && m.ReceiverId == userId2) ||
                         (m.SenderId == userId2 && m.ReceiverId == userId1)) &&
                        m.Content.Contains(searchTerm))
                    .OrderBy(m => m.CreatedAt)
                    .ToListAsync(cancellationToken);

                _logger.LogInformation(
                    "Found {Count} messages matching '{SearchTerm}' between users {UserId1} and {UserId2}",
                    messages.Count,
                    searchTerm,
                    userId1,
                    userId2);

                return messages;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Error searching messages between users {UserId1} and {UserId2} with term '{SearchTerm}'",
                    userId1,
                    userId2,
                    searchTerm);
                throw;
            }
        }
    }
}
