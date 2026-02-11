using HealLink.Application.Interfaces;
using HealLink.Contracts.Notifications;
using Microsoft.AspNetCore.SignalR;

namespace HealLink.Infrastructure.Services;

/// <summary>
/// Implements real-time notification delivery via SignalR
/// Single Responsibility: Only real-time delivery
/// </summary>
public class SignalRNotificationService<THub> : IRealTimeNotificationService 
    where THub : Hub<INotificationClient>
{
    private readonly IHubContext<THub, INotificationClient> _hubContext;
    
    public SignalRNotificationService(IHubContext<THub, INotificationClient> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task SendToUserAsync(Guid userId, NotificationMessage message)
    {
        await _hubContext
            .Clients
            .User(userId.ToString())
            .ReceiveNotification(message);
    }
    
    public async Task SendToUsersAsync(IEnumerable<Guid> userIds, NotificationMessage message)
    {
        var userIdStrings = userIds.Select(id => id.ToString()).ToList();
        await _hubContext
            .Clients
            .Users(userIdStrings)
            .ReceiveNotification(message);
    }
    
    public async Task SendToAllAsync(NotificationMessage message)
    {
        await _hubContext
            .Clients
            .All
            .ReceiveNotification(message);
    }
}
