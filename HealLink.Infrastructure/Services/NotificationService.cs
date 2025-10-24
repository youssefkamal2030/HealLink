using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using HealLink.Application.Interfaces; 
using HealLink.Infrastructure.Notifications.Models;
using HealLink.Infrastructure.Notifications.Hubs;
using HealLink.Infrastructure.Notifications.Interfaces; 

namespace HealLink.Infrastructure.Services
{
   
    public class NotificationService : INotificationService
    {
        
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public NotificationService(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyDoctorOfPendingRequest(Guid doctorId, Guid patientId, Guid connectionRequestId)
        {
         
            var message = new NotificationMessage
            {
                Title = "New Connection Request",
                Body = $"You have a new connection request from Patient {patientId}.",
                Timestamp = DateTime.UtcNow,
                ConnectionRequestId = connectionRequestId,
                PatientId = patientId
            };

           
            await _hubContext
                .Clients
                .User(doctorId.ToString()) 
                .ReceiveNotification(message); 
        }

        public Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId)
        {
            throw new NotImplementedException();
        }
    }
}