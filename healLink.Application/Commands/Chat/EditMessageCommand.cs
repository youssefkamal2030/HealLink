using healLink.Application.Common;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using MediatR;

namespace healLink.Application.Commands.Chat
{
    
    /// <summary>
    /// Command to edit a chat message content.
    /// Only the sender can edit their own message.
    /// </summary>
    public record EditMessageCommand(
        Guid MessageId,
        
        string NewContent
    ) : IRequest<Result<bool>>, IAuthorizeRequest,IResourceOwnerRequest
    {
        public string Policy => AuthorizationPolicies.ResourceOwner;
        public Guid ResourceId => MessageId;

    }
}
