using System;

namespace HealLink.Contracts.Connections.Requests
{
    public record RejectConnectionRequest(Guid ConnectionId, Guid DoctorId);
}
