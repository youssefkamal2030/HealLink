using HealLink.Application.Interfaces;
using HealLink.Contracts.Notifications;
using healLink.Application.DTOs;
using HealLink.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace HealLink.Infrastructure.Services;

/// <summary>
/// Orchestrates notification operations
/// Coordinates both persistence and real-time delivery
/// </summary>
public class NotificationService : INotificationService
{
    private readonly INotificationPersistenceService _persistenceService;
    private readonly IRealTimeNotificationService _realTimeService;
    private readonly HealLinkDbContext _context;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        INotificationPersistenceService persistenceService,
        IRealTimeNotificationService realTimeService,
        HealLinkDbContext context,
        ILogger<NotificationService> logger)
    {
        _persistenceService = persistenceService;
        _realTimeService = realTimeService;
        _context = context;
        _logger = logger;
    }

    public async Task NotifyDoctorOfPendingRequest(Guid doctorId, DoctorConnectionRequestNotificationData data)
    {
        // Get doctor to retrieve UserId for SignalR targeting
        var doctor = await _context.Doctors.FindAsync(doctorId);
        if (doctor == null)
        {
            _logger.LogWarning("Doctor {DoctorId} not found for notification", doctorId);
            return;
        }

        try
        {
            // 1. Persist to database
            await _persistenceService.CreateNotificationForDoctorAsync(
                doctorId,
                "New Connection Request",
                $"You have a new connection request from Patient {data.PatientName}.",
                "ConnectionRequest"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist notification for doctor {DoctorId}", doctorId);
            // Continue to send real-time notification even if persistence fails
        }

        try
        {
            // 2. Send real-time notification
            var message = new NotificationMessage(
                Title: "New Connection Request",
                Body: $"You have a new connection request from Patient {data.PatientName}.",
                Timestamp: DateTime.UtcNow,
                ConnectionRequestId: data.RequestId,
                PatientId: data.PatientId
            );

            await _realTimeService.SendToUserAsync(doctor.UserId, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send real-time notification to doctor {DoctorId}", doctorId);
            // Notification is still persisted in database
        }
    }

    public async Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId)
    {
        // Get patient to retrieve UserId for SignalR targeting
        var patient = await _context.Patients.FindAsync(patientId);
        if (patient == null)
        {
            _logger.LogWarning("Patient {PatientId} not found for notification", patientId);
            return;
        }

        try
        {
            // 1. Persist to database
            await _persistenceService.CreateNotificationForPatientAsync(
                patientId,
                "Connection Accepted",
                "Your connection request has been accepted by the doctor.",
                "ConnectionAccepted"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist notification for patient {PatientId}", patientId);
        }

        try
        {
            // 2. Send real-time notification
            var message = new NotificationMessage(
                Title: "Connection Accepted",
                Body: "Your connection request has been accepted by the doctor.",
                Timestamp: DateTime.UtcNow,
                DoctorId: doctorId
            );

            await _realTimeService.SendToUserAsync(patient.UserId, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send real-time notification to patient {PatientId}", patientId);
        }
    }

    public async Task NotifyPatientOfRejection(Guid patientId, Guid doctorId)
    {
        // Get patient to retrieve UserId for SignalR targeting
        var patient = await _context.Patients.FindAsync(patientId);
        if (patient == null)
        {
            _logger.LogWarning("Patient {PatientId} not found for notification", patientId);
            return;
        }

        try
        {
            // 1. Persist to database
            await _persistenceService.CreateNotificationForPatientAsync(
                patientId,
                "Connection Rejected",
                "Your connection request has been rejected by the doctor.",
                "ConnectionRejected"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist notification for patient {PatientId}", patientId);
        }

        try
        {
            // 2. Send real-time notification
            var message = new NotificationMessage(
                Title: "Connection Rejected",
                Body: "Your connection request has been rejected by the doctor.",
                Timestamp: DateTime.UtcNow,
                DoctorId: doctorId
            );

            await _realTimeService.SendToUserAsync(patient.UserId, message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send real-time notification to patient {PatientId}", patientId);
        }
    }
}
