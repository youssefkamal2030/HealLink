using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using HealLink.Application.Interfaces; 
using HealLink.Infrastructure.Notifications.Models;
using HealLink.Infrastructure.Notifications.Hubs;
using HealLink.Infrastructure.Notifications.Interfaces;
using healLink.Application.DTOs;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Services
{
   
    public class NotificationService : INotificationService
    {
        
        private readonly IHubContext<NotificationHub, INotificationClient> _hubContext;
        private readonly INotificationRepository _notificationRepository;
        private readonly HealLinkDbContext _context;

        public NotificationService(
            IHubContext<NotificationHub, INotificationClient> hubContext,
            INotificationRepository notificationRepository,
            HealLinkDbContext context)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
            _context = context;
        }

        public async Task NotifyDoctorOfPendingRequest(Guid doctorId, DoctorConnectionRequestNotificationData data)
        {
            // Get doctor to retrieve UserId for SignalR targeting
            var doctor = await _context.Doctors.FindAsync(doctorId);
            if (doctor == null) return; // Doctor not found

            // 1. Create and persist notification entity to database using Doctor entity ID
            var notification = Notification.ForDoctor(
                doctorId: doctorId,
                title: "New Connection Request",
                message: $"You have a new connection request from Patient {data.PatientName}.",
                type: "ConnectionRequest",
                relatedPatientId: data.PatientId,
                connectionId: data.RequestId
            );
            
            await _notificationRepository.CreateNotificationAsync(notification);

            // 2. Send real-time notification via SignalR using doctor's UserId
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
                .User(doctor.UserId.ToString()) // Use UserId for SignalR
                .ReceiveNotification(message);
        }

        public async Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId)
        {
            // Get patient to retrieve UserId for SignalR targeting
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null) return; // Patient not found

            // 1. Create and persist notification entity to database using Patient entity ID
            var notification = Notification.ForPatient(
                patientId: patientId,
                title: "Connection Accepted",
                message: "Your connection request has been accepted by the doctor.",
                type: "ConnectionAccepted",
                relatedDoctorId: doctorId
            );
            
            await _notificationRepository.CreateNotificationAsync(notification);

            // 2. Send real-time notification via SignalR using patient's UserId
            var message = new NotificationMessage
            {
                Title = "Connection Accepted",
                Body = "Your connection request has been accepted by the doctor.",
                Timestamp = DateTime.UtcNow,
                DoctorId = doctorId
            };

            await _hubContext
                .Clients
                .User(patient.UserId.ToString()) // Use UserId for SignalR
                .ReceiveNotification(message);
        }

        public async Task NotifyPatientOfRejection(Guid patientId, Guid doctorId)
        {
            // Get patient to retrieve UserId for SignalR targeting
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null) return; // Patient not found

            // 1. Create and persist notification entity to database using Patient entity ID
            var notification = Notification.ForPatient(
                patientId: patientId,
                title: "Connection Rejected",
                message: "Your connection request has been rejected by the doctor.",
                type: "ConnectionRejected",
                relatedDoctorId: doctorId
            );
            
            await _notificationRepository.CreateNotificationAsync(notification);

            // 2. Send real-time notification via SignalR using patient's UserId
            var message = new NotificationMessage
            {
                Title = "Connection Rejected",
                Body = "Your connection request has been rejected by the doctor.",
                Timestamp = DateTime.UtcNow,
                DoctorId = doctorId
            };

            await _hubContext
                .Clients
                .User(patient.UserId.ToString()) // Use UserId for SignalR
                .ReceiveNotification(message);
        }
    }
}
