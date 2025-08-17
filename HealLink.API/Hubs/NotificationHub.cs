using Microsoft.AspNetCore.SignalR;
namespace HealLink.API.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotificationToDoctor(string doctorId, string message)
        {
            await Clients.User(doctorId).SendAsync("ReceiveNotification", message);
        }
    }
}