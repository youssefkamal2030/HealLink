using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Auth
{
    /// <summary>
    /// Command to change password for an authenticated user.
    /// Requires current password verification.
    /// </summary>
    public record ChangePasswordCommand(
        Guid UserId,
        string CurrentPassword,
        string NewPassword
    ) : IRequest<Result<bool>>;
}
