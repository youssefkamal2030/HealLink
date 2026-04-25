using healLink.Application.Common.Models;
using MediatR;

namespace healLink.Application.Commands.Connections
{
    /// <summary>
    /// Command to terminate an accepted connection between a doctor and patient.
    /// Either party (doctor or patient) can terminate the connection.
    /// </summary>
    public record TerminateConnectionCommand(
        Guid ConnectionId,
        Guid RequestingUserId
    ) : IRequest<Result<bool>>;
}
