using healLink.Application.Common.Models;
using HealLink.Contracts.Chat.Responses;
using MediatR;

namespace healLink.Application.Queries.Chat
{
    /// <summary>
    /// Query to search messages between two users by content.
    /// </summary>
    public record SearchMessagesQuery(
        Guid UserId1,
        Guid UserId2,
        string SearchTerm
    ) : IRequest<Result<List<ChatMessageDto>>>;
}
