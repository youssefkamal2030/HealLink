using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Profile
{
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
