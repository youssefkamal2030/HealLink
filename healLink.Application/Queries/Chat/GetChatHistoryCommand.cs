using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Queries.Chat
{
    public record GetChatHistoryCommand( Guid UserId1, Guid UserId2 ) : IRequest<Result<List<MessageDto>>>;  
}