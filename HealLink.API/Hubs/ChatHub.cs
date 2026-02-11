using HealLink.Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;

namespace HealLink.API.Hubs;

/// <summary>
/// SignalR hub for real-time chat functionality
/// Placeholder for future chat feature implementation
/// </summary>
public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly HealLinkDbContext _dbContext;

    public ChatHub(ILogger<ChatHub> logger, HealLinkDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task SendMessage(string userId, string message)
    {
        _logger.LogInformation("User {UserId} sending message", userId);
        // Broadcast the message to all clients except the sender
        await Clients.Others.SendAsync("ReceiveMessage", userId, message);
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("User connected to ChatHub. ConnectionId: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (exception != null)
        {
            _logger.LogError(exception, "User disconnected from ChatHub with error. ConnectionId: {ConnectionId}", Context.ConnectionId);
        }
        else
        {
            _logger.LogInformation("User disconnected from ChatHub. ConnectionId: {ConnectionId}", Context.ConnectionId);
        }
        await base.OnDisconnectedAsync(exception);
    }
}
