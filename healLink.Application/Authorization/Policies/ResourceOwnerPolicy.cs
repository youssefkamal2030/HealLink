using healLink.Application.Common;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;

namespace healLink.Application.Authorization.Policies;

/// <summary>
/// Authorization policy that verifies the current user owns the resource being acted upon.
/// Currently supports ChatMessage resources (checks if currentUser.UserId == message.SenderId).
/// 
/// USAGE:
/// Commands implement IAuthorizeRequest with Policy = "ResourceOwner" and IResourceOwnerRequest with ResourceId.
/// Example: EditMessageCommand(MessageId, NewContent) where ResourceId = MessageId
/// 
/// FLOW:
/// 1. Check if request implements IResourceOwnerRequest (provides ResourceId)
/// 2. Load the ChatMessage by ResourceId from the repository
/// 3. Compare message.SenderId with currentUser.UserId
/// 4. Return true if they match (user owns the resource), false otherwise
/// 5. Return false if resource not found (treat as unauthorized, not 404)
/// </summary>
public class ResourceOwnerPolicy : IAuthorizationPolicy
{
    private readonly IChatRepository _chatRepository;

    public ResourceOwnerPolicy(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    /// <summary>
    /// The policy name that commands reference via IAuthorizeRequest.Policy.
    /// Must match AuthorizationPolicies.ResourceOwner constant.
    /// </summary>
    public string Name => AuthorizationPolicies.ResourceOwner;

    /// <summary>
    /// Determines if the current user is authorized to access the resource.
    /// </summary>
    /// <param name="currentUser">Service providing the authenticated user's ID from JWT claims</param>
    /// <param name="request">The command/query being executed (must implement IResourceOwnerRequest)</param>
    /// <param name="cancellationToken">Cancellation token for async operations</param>
    /// <returns>
    /// True if current user owns the resource (currentUser.UserId == resource.OwnerId).
    /// False if user doesn't own the resource, resource not found, or request doesn't implement IResourceOwnerRequest.
    /// </returns>
    public async Task<bool> AuthorizeAsync(
        ICurrentUserService currentUser,
        object request,
        CancellationToken cancellationToken)
    {
        // STEP 1: Verify request provides a ResourceId
        if (request is not IResourceOwnerRequest ownerRequest)
            return false;

        // STEP 2: Load the resource (currently only ChatMessage is supported)
        // Future: Add support for other resource types by checking request type and dispatching to appropriate repository
        var message = await _chatRepository.GetByIdAsync(ownerRequest.ResourceId, cancellationToken);

        // STEP 3: Return false if resource doesn't exist (treat as unauthorized, not 404)
        if (message == null)
            return false;

        // STEP 4: Check if current user owns the resource
        // For ChatMessage: owner is the SenderId
        return message.SenderId == currentUser.UserId;
    }
}
