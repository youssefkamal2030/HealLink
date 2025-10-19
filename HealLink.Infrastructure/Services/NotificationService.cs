//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HealLink.Application.Interfaces;
//using Microsoft.AspNetCore.SignalR;

//namespace HealLink.Infrastructure.Services
//{
//    public class NotificationService : INotificationService
//    {
//        private readonly IHubContext<NotificationHub> _hubContext;

//        public NotificationService(IHubContext<NotificationHub> hubContext)
//        {
//            _hubContext = hubContext;
//        }

//        public Task NotifyDoctorOfPendingRequest(Guid doctorId, Guid patientId, Guid connectionId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId)
//        {
//            throw new NotImplementedException();
//        }

//        public Task NotifyPatientOfRejection(Guid patientId, Guid doctorId)
//        {
//            throw new NotImplementedException();
//        }

//        public async Task SendDoctorConnectionRequestNotification(string doctorId, string patientId)
//        {
//            //var message = $"Patient {patientId} has requested to connect with you.";
//            //await _hubContext.Clients.User(doctorId).SendAsync("ReceiveNotification", message);
//            throw new NotImplementedException();
//        }
//    }
//}
