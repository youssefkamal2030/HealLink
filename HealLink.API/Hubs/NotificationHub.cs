using HealLink.Contracts.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HealLink.API.Hubs;

/// <summary>
/// SignalR hub for real-time notifications
/// Handles client connections and server-to-client communication
/// Requires authentication for all operations
/// </summary>
[Authorize]
public class NotificationHub : Hub<INotificationClient>
{
    private readonly ILogger<NotificationHub> _logger;

    public NotificationHub(ILogger<NotificationHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        _logger.LogInformation("User {UserId} connected to NotificationHub. ConnectionId: {ConnectionId}", 
            userId, Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (exception != null)
        {
            _logger.LogError(exception, "User {UserId} disconnected from NotificationHub with error. ConnectionId: {ConnectionId}", 
                userId, Context.ConnectionId);
        }
        else
        {
            _logger.LogInformation("User {UserId} disconnected from NotificationHub. ConnectionId: {ConnectionId}", 
                userId, Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
