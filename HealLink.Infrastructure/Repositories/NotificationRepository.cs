using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using HealLink.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HealLink.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly HealLinkDbContext _context;

        public NotificationRepository(HealLinkDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> CreateNotificationAsync(Notification notification)
        {
            if (notification == null) throw new ArgumentNullException(nameof(notification));
            await _context.Notifications.AddAsync(notification);
            return notification;
        }

        public async Task<List<Notification>> GetDoctorNotificationsAsync(Guid doctorId)
            => await _context.Notifications
                .Where(n => n.RecipientId == doctorId && n.RecipientType == HealLink.Domain.Enums.RecipientType.Doctor)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

        public async Task<List<Notification>> GetPatientNotificationsAsync(Guid patientId)
            => await _context.Notifications
                .Where(n => n.RecipientId == patientId && n.RecipientType == HealLink.Domain.Enums.RecipientType.Patient)
                .OrderByDescending(n => n.CreatedAt)
                .ToListAsync();

        public async Task<Notification> GetByIdAsync(Guid id)
            => await _context.Notifications.FindAsync(id);

        public void UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
        }

        public void DeleteByRecipient(Guid recipientId, RecipientType recipientType)
        {
            var notifications =  _context.Notifications.Where(n => n.RecipientId == recipientId && n.RecipientType == recipientType);
            _context.Notifications.RemoveRange(notifications);
       

        }
    }
}
