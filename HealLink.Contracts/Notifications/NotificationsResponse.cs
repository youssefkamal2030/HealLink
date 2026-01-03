using System;

namespace HealLink.Contracts.Notifications
{
    public record NotificationResponse(
        Guid Id,
        string Title,
        string Message,
        string Type,
        bool IsRead,
        DateTime? ReadAt,
        DateTime CreatedAt
    );

    public record NotificationsListResponse(
        bool Success,
        string Message,
        List<NotificationResponse> Notifications,
        int TotalCount
    );
}
