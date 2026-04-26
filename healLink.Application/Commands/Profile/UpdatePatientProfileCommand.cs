using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    // TODO: [REFACTOR-AUTH] Add authorization attribute after centralized-authorization-infrastructure is implemented
    // PROBLEM: Authorization is currently handled inline in handler (checks patient.UserId == request.AuthenticatedUserId)
    // FIX: Add [Authorize(AuthorizationPolicies.ResourceOwner)] attribute to this command
    // APPROACH: AuthorizationBehavior will check if current user owns the patient profile
    // REASON: Centralize authorization in application layer via pipeline behavior
    // MIGRATION STEPS:
    //   1. Add: [Authorize(AuthorizationPolicies.ResourceOwner)]
    //   2. Remove AuthenticatedUserId from command (will be extracted from JWT by UserContextProvider)
    //   3. Remove authorization check from handler (lines 32-35 in UpdatePatientProfileCommandHandler)
    //   4. Update controller to not extract JWT claims manually
    /// <summary>
    /// Command to update a patient's basic profile information.
    /// Currently updates User.Username and User.Email.
    /// </summary>
    public record UpdatePatientProfileCommand(
        Guid PatientId,
        Guid AuthenticatedUserId,
        string Username,
        string Email
    ) : IRequest<Result<bool>>;
}
