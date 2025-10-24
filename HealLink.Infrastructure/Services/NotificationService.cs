using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using HealLink.Application.Interfaces; 
using HealLink.Infrastructure.Notifications.Models;
using HealLink.Infrastructure.Notifications.Hubs;
using HealLink.Infrastructure.Notifications.Interfaces;
using healLink.Application.DTOs;

namespace HealLink.Infrastructure.Services
{
   
    public class NotificationService : INotificationService
    {
        
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;

        public NotificationService(IHubContext<NotificationHub, INotificationClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyDoctorOfPendingRequest(Guid doctorId, DoctorConnectionRequestNotificationData data)
        {
            var message = new NotificationMessage
            {
                Title = "New Connection Request",
                Body = $"You have a new connection request from Patient {data.PatientName}.",
                Timestamp = DateTime.UtcNow,
                ConnectionRequestId = data.RequestId,
                PatientId = data.PatientId,
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