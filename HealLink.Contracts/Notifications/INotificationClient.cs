namespace HealLink.Contracts.Notifications;

/// <summary>
/// SignalR client interface for type-safe hub communication
/// </summary>
public interface INotificationClient
{
    /// <summary>
    /// Receives a real-time notification from the server
    /// </summary>
    Task ReceiveNotification(NotificationMessage message);
}
