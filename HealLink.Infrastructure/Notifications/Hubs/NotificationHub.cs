using HealLink.Infrastructure.Notifications.Interfaces;
using MediatR;
using Microsoft.AspNetCore.SignalR;
namespace HealLink.Infrastructure.Notifications.Hubs
{
    public class NotificationHub : Hub<INotificationClient>
    {
        public async Task SendNotificationToDoctor(string doctorId, string message)
        {
           
        }
    }
}