using healLink.Application.Common.Models;
using HealLink.Contracts.Chat.Responses;
using MediatR;

namespace healLink.Application.Queries.Chat
{
    public record GetChatHistoryQuery(Guid UserId1, Guid UserId2) : IRequest<Result<ChatHistoryResponse>>;
}
