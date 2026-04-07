using System;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;

namespace HealLink.Infrastructure.Services;

public class NotificationPersistenceService : INotificationPersistenceService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public NotificationPersistenceService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Notification> CreateNotificationForDoctorAsync(Guid doctorId, string title, string message, NotificationType type)
    {
        var notification = Notification.ForDoctor(doctorId, title, message, type);
        await _notificationRepository.CreateNotificationAsync(notification);
        await _unitOfWork.SaveChangesAsync();
        return notification;
    }

    public async Task<Notification> CreateNotificationForPatientAsync(Guid patientId, string title, string message, NotificationType type)
    {
        var notification = Notification.ForPatient(patientId, title, message, type);
        await _notificationRepository.CreateNotificationAsync(notification);
        await _unitOfWork.SaveChangesAsync();
        return notification;
    }
}
