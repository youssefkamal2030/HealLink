using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Chat
{
    // TODO: [REFACTOR-AUTH] Add authorization attribute after centralized-authorization-infrastructure is implemented
    // PROBLEM: Authorization is currently handled in domain entity (ChatMessage.SoftDelete checks requestingUserId)
    // FIX: Add [Authorize(AuthorizationPolicies.ResourceOwner)] attribute to this command
    // APPROACH: AuthorizationBehavior will check if current user's UserId matches message.SenderId
    // REASON: Centralize authorization in application layer, remove from domain
    // MIGRATION STEPS:
    //   1. Add: [Authorize(AuthorizationPolicies.ResourceOwner)]
    //   2. Remove RequestingUserId from command (will be extracted from JWT by UserContextProvider)
    //   3. Update handler to not pass requestingUserId to domain method
    //   4. Update controller to not extract JWT claims manually
    /// <summary>
    /// Command to soft delete a chat message.
    /// Only the sender can delete their own message.
    /// </summary>
    public record DeleteMessageCommand(
        Guid MessageId,
        Guid RequestingUserId
    ) : IRequest<Result<bool>>;
}
