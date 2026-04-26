using healLink.Application.Common.Models;
using healLink.Application.Queries.Chat;
using healLink.Application.Repositories;
using HealLink.Contracts.Chat.Responses;
using MediatR;

namespace healLink.Application.Handlers.Chat
{
    public class SearchMessagesQueryHandler : IRequestHandler<SearchMessagesQuery, Result<List<ChatMessageDto>>>
    {
        private readonly IChatRepository _chatRepository;

        public SearchMessagesQueryHandler(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<Result<List<ChatMessageDto>>> Handle(SearchMessagesQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.SearchTerm))
                return Result<List<ChatMessageDto>>.Failure("Search term cannot be empty.");

            var messages = await _chatRepository.SearchMessagesAsync(
                request.UserId1,
                request.UserId2,
                request.SearchTerm,
                cancellationToken);

            if (messages == null || messages.Count == 0)
                return Result<List<ChatMessageDto>>.Success(new List<ChatMessageDto>());

            var messageDtos = messages
                .Where(m => !m.IsDeleted) // Exclude deleted messages from search results
                .Select(m => new ChatMessageDto(
                    m.Id,
                    m.SenderId,
                    m.ReceiverId,
                    m.Content,
                    m.Status.ToString(),
                    m.CreatedAt,
                    m.DeliveredAt,
                    m.ReadAt))
                .ToList();

            return Result<List<ChatMessageDto>>.Success(messageDtos);
        }
    }
}
