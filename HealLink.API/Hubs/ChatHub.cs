using healLink.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace HealLink.API.Hubs;

/// <summary>
/// SignalR hub for real-time chat functionality
/// Requires authentication for all operations
/// </summary>
[Authorize]
public class ChatHub(ILogger<ChatHub> logger, IChatService chatService) : Hub
{
    private readonly ILogger<ChatHub> _logger = logger;
    private readonly IChatService _chatService = chatService;

 

    public async Task SendMessage(Guid senderId, Guid receiverId, string message)
    {
        try
        {
            var authenticatedUserId = Context.UserIdentifier;
            if (authenticatedUserId != senderId.ToString())
            {
                
                await Clients.Caller.SendAsync("MessageFailed", "Cannot send messages as another user");
                return;
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                await Clients.Caller.SendAsync("MessageFailed", "Message cannot be empty");
                return;
            }
            if(await _chatService.ValidateConnection(senderId, receiverId))
            {
                await Clients.Caller.SendAsync("MessageFailed", "No connection exists between sender and receiver");
                return;
            }
            await _chatService.SendMessageAsync(senderId, receiverId, message);
            await Clients.User(receiverId.ToString())
                .SendAsync("ReceiveMessage", senderId, message, DateTime.UtcNow);
            await Clients.Caller.SendAsync("MessageSent", senderId, receiverId, message, DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send message from {SenderId} to {ReceiverId}", 
                senderId, receiverId);
            
            await Clients.Caller.SendAsync("MessageFailed", "Failed to send message. Please try again.");
        }
    }

    public async Task GetChatHistory(Guid userId1, Guid userId2)
    {
        try
        {
            // Validate authenticated user is one of the participants
            var authenticatedUserId = Context.UserIdentifier;
            if (authenticatedUserId != userId1.ToString() && authenticatedUserId != userId2.ToString())
            {
                _logger.LogWarning("User {AuthUserId} attempted to access chat history between {UserId1} and {UserId2}", 
                    authenticatedUserId, userId1, userId2);
                await Clients.Caller.SendAsync("ChatHistoryFailed", "Unauthorized access to chat history");
                return;
            }

            var messages = await _chatService.GetChatHistoryAsync(userId1, userId2);
            
            _logger.LogInformation("Retrieved {Count} messages for users {UserId1} and {UserId2}", 
                messages.Count, userId1, userId2);

            await Clients.Caller.SendAsync("ChatHistoryReceived", messages);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve chat history between {UserId1} and {UserId2}", 
                userId1, userId2);
            
            await Clients.Caller.SendAsync("ChatHistoryFailed", "Failed to retrieve chat history");
        }
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
