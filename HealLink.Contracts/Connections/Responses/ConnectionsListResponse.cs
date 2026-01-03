using System;
using System.Collections.Generic;

namespace HealLink.Contracts.Connections.Responses
{
    public record ConnectionResponse(
        Guid Id,
        Guid DoctorId,
        Guid PatientId,
        string Status,
        DateTime CreatedAt,
        DateTime? AcceptedAt
    );

    public record ConnectionsListResponse(
        bool Success,
        string Message,
        List<ConnectionResponse> Connections,
        int TotalCount
    );
}
