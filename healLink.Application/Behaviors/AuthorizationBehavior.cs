using healLink.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior that enforces authorization policies before command/query handlers execute.
    /// This behavior runs AFTER ValidationBehavior and BEFORE the actual handler in the pipeline.
    /// 
    /// PIPELINE ORDER: Controller → ValidationBehavior → AuthorizationBehavior → Handler → Domain Entity
    /// 
    /// HOW IT WORKS:
    /// 1. Checks if the request implements IAuthorizeRequest (opt-in authorization)
    /// 2. If NO → passes through to next behavior/handler (no authorization needed)
    /// 3. If YES → resolves the policy by name from DI container
    /// 4. Calls policy.AuthorizeAsync() to check if current user is authorized
    /// 5. If authorized (true) → continues to next behavior/handler
    /// 6. If NOT authorized (false) → throws UnauthorizedAccessException (HTTP 403)
    /// 
    /// EXAMPLE USAGE:
    /// A command like EditMessageCommand implements:
    /// - IAuthorizeRequest { Policy => "ResourceOwner" }
    /// - IResourceOwnerRequest { ResourceId => MessageId }
    /// 
    /// This behavior will:
    /// 1. Detect IAuthorizeRequest
    /// 2. Find ResourceOwnerPolicy by name "ResourceOwner"
    /// 3. Call ResourceOwnerPolicy.AuthorizeAsync(currentUser, request, ct)
    /// 4. Policy loads the message and checks if currentUser.UserId == message.SenderId
    /// 5. If match → continue; if not → throw UnauthorizedAccessException
    /// </summary>
    /// <typeparam name="TRequest">The MediatR request type (command or query) - must implement IRequest&lt;TResponse&gt;</typeparam>
    /// <typeparam name="TResponse">The response type returned by the handler (e.g., Result&lt;bool&gt;, Result&lt;ChatMessageDto&gt;)</typeparam>
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        /// <summary>
        /// Service that provides the authenticated user's ID from the current HTTP context.
        /// Reads JWT claims (sub or NameIdentifier) and returns Guid.Empty if no user is authenticated.
        /// </summary>
        private readonly ICurrentUserService _currentUser;

        /// <summary>
        /// Collection of all registered authorization policies (ResourceOwnerPolicy, PatientOrGuardianAccessPolicy, etc.).
        /// Injected from DI container - all IAuthorizationPolicy implementations are registered at startup.
        /// </summary>
        private readonly IEnumerable<IAuthorizationPolicy> _policies;

        /// <summary>
        /// Main pipeline method that intercepts every MediatR request before it reaches the handler.
        /// </summary>
        /// <param name="request">The incoming command or query (e.g., EditMessageCommand, GetChatHistoryQuery)</param>
        /// <param name="next">Delegate to invoke the next behavior/handler in the pipeline</param>
        /// <param name="cancellationToken">Cancellation token for async operations</param>
        /// <returns>The response from the handler (TResponse)</returns>
        /// <exception cref="InvalidOperationException">Thrown when the policy name in request.Policy doesn't match any registered IAuthorizationPolicy</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when policy.AuthorizeAsync returns false (user not authorized)</exception>
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            // STEP 1: Check if this request requires authorization
            // If request does NOT implement IAuthorizeRequest, skip authorization entirely
            if (request is not IAuthorizeRequest authorizeRequest)
                return await next();

            // STEP 2: Resolve the policy by name from the DI container
            // Example: request.Policy = "ResourceOwner" → finds ResourceOwnerPolicy
            // Throws InvalidOperationException if policy name is not registered
            var policy = _policies.FirstOrDefault(p => p.Name == authorizeRequest.Policy)
                ?? throw new InvalidOperationException(
                    $"Authorization policy '{authorizeRequest.Policy}' is not registered.");

            // STEP 3: Execute the policy to check if current user is authorized
            // Policy receives:
            // - _currentUser: contains UserId from JWT claims
            // - request: the full command/query object (policy can cast to IResourceOwnerRequest, IPatientScopedRequest, etc.)
            // - cancellationToken: for async operations
            // Returns: true if authorized, false if not
            var authorized = await policy.AuthorizeAsync(_currentUser, request, cancellationToken);

            // STEP 4: Enforce the authorization decision
            // If NOT authorized → throw UnauthorizedAccessException (mapped to HTTP 403 by ExceptionHandlingMiddleware)
            if (!authorized)
                throw new UnauthorizedAccessException(
                    $"Access denied. Policy '{authorizeRequest.Policy}' was not satisfied.");

            // STEP 5: User is authorized → continue to next behavior/handler
            return await next();
        }
    }
}
