using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    // TODO: [REFACTOR-AUTH] Add authorization attribute after centralized-authorization-infrastructure is implemented
    // PROBLEM: Authorization is currently handled inline in handler (checks isOwner || isAdmin)
    // FIX: Add [Authorize(AuthorizationPolicies.ResourceOwnerOrAdmin)] attribute to this command
    // APPROACH: AuthorizationBehavior will check if current user owns the patient profile OR is Admin
    // REASON: Centralize authorization in application layer via pipeline behavior
    // MIGRATION STEPS:
    //   1. Add: [Authorize(AuthorizationPolicies.ResourceOwnerOrAdmin)]
    //   2. Remove AuthenticatedUserId from command (will be extracted from JWT by UserContextProvider)
    //   3. Remove authorization check from handler (lines 38-44 in DeletePatientProfileCommandHandler)
    //   4. Update controller to not extract JWT claims manually
    //   5. Create ResourceOwnerOrAdminPolicy that checks ownership OR Admin role
    /// <summary>
    /// Command to delete a patient profile.
    /// Only the patient themselves or an admin can delete the profile.
    /// </summary>
    public record DeletePatientProfileCommand(
        Guid PatientId,
        Guid AuthenticatedUserId
    ) : IRequest<Result<bool>>;
}
