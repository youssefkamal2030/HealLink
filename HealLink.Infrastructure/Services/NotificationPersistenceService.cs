using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.Entities;

namespace HealLink.Infrastructure.Services;

/// <summary>
/// Handles database persistence of notifications
/// Single Responsibility: Only database operations
/// </summary>
public class NotificationPersistenceService : INotificationPersistenceService
{
    private readonly INotificationRepository _notificationRepository;
    
    public NotificationPersistenceService(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    
    public async Task<Notification> CreateNotificationForDoctorAsync(
        Guid doctorId, 
        string title, 
        string message, 
        string type)
    {
        var notification = Notification.ForDoctor(doctorId, title, message, type);
        await _notificationRepository.CreateNotificationAsync(notification);
        return notification;
    }
    
    public async Task<Notification> CreateNotificationForPatientAsync(
        Guid patientId, 
        string title, 
        string message, 
        string type)
    {
        var notification = Notification.ForPatient(patientId, title, message, type);
        await _notificationRepository.CreateNotificationAsync(notification);
        return notification;
    }
}
