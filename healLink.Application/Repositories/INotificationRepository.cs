using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace healLink.Application.Repositories
{
    //Todo : this should inherit from the generic repository interface to reduce code duplication 

    public interface INotificationRepository
    {
        Task<Notification> CreateNotificationAsync(Notification notification);
        Task<List<Notification>> GetDoctorNotificationsAsync(Guid doctorId);
        Task<List<Notification>> GetPatientNotificationsAsync(Guid patientId);
        Task<Notification> GetByIdAsync(Guid id);
        void DeleteByRecipient(Guid recipientId, RecipientType recipientType);

        void UpdateNotification(Notification notification);

    }
}
