using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    /// <summary>
    /// Command to delete a patient profile.
    /// Only the patient themselves or an admin can delete the profile.
    /// </summary>
    public record DeletePatientProfileCommand(
        Guid PatientId,
        Guid AuthenticatedUserId
    ) : IRequest<Result<bool>>;
}
