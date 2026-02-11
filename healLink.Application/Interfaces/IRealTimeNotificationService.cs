using HealLink.Contracts.Notifications;

namespace HealLink.Application.Interfaces;

/// <summary>
/// Abstraction for real-time notification delivery
/// Decouples application logic from SignalR infrastructure
/// </summary>
public interface IRealTimeNotificationService
{
    /// <summary>
    /// Sends a real-time notification to a specific user
    /// </summary>
    /// <param name="userId">The user's ID (from User entity)</param>
    /// <param name="message">The notification message</param>
    Task SendToUserAsync(Guid userId, NotificationMessage message);
    
    /// <summary>
    /// Sends a real-time notification to multiple users
    /// </summary>
    /// <param name="userIds">Collection of user IDs</param>
    /// <param name="message">The notification message</param>
    Task SendToUsersAsync(IEnumerable<Guid> userIds, NotificationMessage message);
    
    /// <summary>
    /// Sends a real-time notification to all connected clients
    /// </summary>
    /// <param name="message">The notification message</param>
    Task SendToAllAsync(NotificationMessage message);
}
