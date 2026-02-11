using healLink.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace HealLink.API.Hubs;

/// <summary>
/// SignalR hub for real-time chat functionality
/// Placeholder for future chat feature implementation
/// </summary>
public class ChatHub(ILogger<ChatHub> logger, IChatService chatService) : Hub
{
    private readonly ILogger<ChatHub> _logger = logger;
    private readonly IChatService _chatService = chatService;

 

    public async Task SendMessage(Guid senderID , Guid reciverID, string message)
    {
        _logger.LogInformation("User {senderID} sending message", senderID);
        // Broadcast the message to all clients except the sender
        _chatService.SendMessageAsync( senderID,reciverID, message);
        await Clients.Others.SendAsync("ReceiveMessage", senderID, message);
        
        
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
