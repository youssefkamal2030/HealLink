using System;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Data;

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
            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
            
            return notification;
        }
    }
}
