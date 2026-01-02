using System;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotificationAsync(Notification notification);
    }
}
