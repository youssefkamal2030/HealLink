using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealLink.Domain.Entities;

namespace healLink.Application.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<List<Notification>> GetDoctorNotificationsAsync(Guid doctorId);
        Task<List<Notification>> GetPatientNotificationsAsync(Guid patientId);
        Task<Notification> GetByIdAsync(Guid id);
        Task UpdateNotificationAsync(Notification notification);
    }
}
