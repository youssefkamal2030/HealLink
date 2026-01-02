using System;

namespace HealLink.Contracts.Connections.Requests
{
    public record AcceptConnectionRequest(Guid ConnectionId, Guid DoctorId);
}
