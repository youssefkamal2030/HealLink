using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Connections.Responses;
using MediatR;

namespace healLink.Application.Commands.Connections
{
    public record AcceptConnectionCommand(
        Guid ConnectionId,
        Guid DoctorId
    ) : IRequest<Result<ConnectionActionResponse>>;
}

