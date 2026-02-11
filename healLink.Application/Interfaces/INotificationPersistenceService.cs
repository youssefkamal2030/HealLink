using HealLink.Domain.Entities;

namespace HealLink.Application.Interfaces;

/// <summary>
/// Abstraction for notification persistence operations
/// Handles database storage of notifications
/// </summary>
public interface INotificationPersistenceService
{
    /// <summary>
    /// Creates and persists a notification for a doctor
    /// </summary>
    /// <param name="doctorId">The doctor's ID</param>
    /// <param name="title">Notification title</param>
    /// <param name="message">Notification message</param>
    /// <param name="type">Notification type</param>
    /// <returns>The created notification entity</returns>
    Task<Notification> CreateNotificationForDoctorAsync(
        Guid doctorId, 
        string title, 
        string message, 
        string type);
    
    /// <summary>
    /// Creates and persists a notification for a patient
    /// </summary>
    /// <param name="patientId">The patient's ID</param>
    /// <param name="title">Notification title</param>
    /// <param name="message">Notification message</param>
    /// <param name="type">Notification type</param>
    /// <returns>The created notification entity</returns>
    Task<Notification> CreateNotificationForPatientAsync(
        Guid patientId, 
        string title, 
        string message, 
        string type);
}
