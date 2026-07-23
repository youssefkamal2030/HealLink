using System;
using System.Threading.Tasks;
using healLink.Application.DTOs;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Contracts.Notifications;
using HealLink.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace HealLink.Infrastructure.Services;

/// <summary>
/// Orchestrates notification operations — coordinates persistence and real-time delivery.
/// Uses repositories for entity lookups so it has no direct DbContext dependency.
/// </summary>
public class NotificationService : INotificationService
{
    private readonly INotificationPersistenceService _persistenceService;
    private readonly IRealTimeNotificationService _realTimeService;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(
        INotificationPersistenceService persistenceService,
        IRealTimeNotificationService realTimeService,
        IDoctorRepository doctorRepository,
        IPatientRepository patientRepository,
        ILogger<NotificationService> logger)
    {
        _persistenceService = persistenceService;
        _realTimeService = realTimeService;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _logger = logger;
    }

    public async Task NotifyDoctorOfPendingRequest(Guid doctorId, DoctorConnectionRequestNotificationData data)
    {
        var doctor = await _doctorRepository.GetByDoctorId(doctorId);
        if (doctor == null)
        {
            _logger.LogWarning("Doctor {DoctorId} not found for notification", doctorId);
            return;
        }

        try
        {
            await _persistenceService.CreateNotificationForDoctorAsync(
                doctorId,
                "New Connection Request",
                $"You have a new connection request from Patient {data.PatientName}.",
                NotificationType.ConnectionRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist notification for doctor {DoctorId}", doctorId);
        }

        try
        {
            await _realTimeService.SendToUserAsync(doctor.UserId, new NotificationMessage(
                Title: "New Connection Request",
                Body: $"You have a new connection request from Patient {data.PatientName}.",
                Timestamp: DateTime.UtcNow,
                ConnectionRequestId: data.RequestId,
                PatientId: data.PatientId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send real-time notification to doctor {DoctorId}", doctorId);
        }
    }
    public async Task NotifyDoctorOfRejection(Guid doctorId, string reason)
    {
        var doctor = await _doctorRepository.GetByDoctorId(doctorId);
        if (doctor == null)
        {
            _logger.LogWarning("Doctor {DoctorId} not found for rejection notification", doctorId);
            return;
        }
        try
        {
            await _persistenceService.CreateNotificationForDoctorAsync(
                doctorId,
                "Account Rejected",
                $"Your account has been rejected. Reason: {reason}",
                NotificationType.DoctorRejected);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist rejection notification for doctor {DoctorId}", doctorId);
        }
    }

    public async Task NotifyDoctorOfApproval(Guid doctorId)
    {
        var doctor = await _doctorRepository.GetByDoctorId(doctorId);
        if (doctor == null)
        {
            _logger.LogWarning("Doctor {DoctorId} not found for approval notification", doctorId);
            return;
        }

        try
        {
            await _persistenceService.CreateNotificationForDoctorAsync(
                doctorId,
                "Account Approved",
                "Your account has been approved. You can now accept patient connections.",
                NotificationType.DoctorApproved);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist approval notification for doctor {DoctorId}", doctorId);
        }

        try
        {
            await _realTimeService.SendToUserAsync(doctor.UserId, new NotificationMessage(
                Title: "Account Approved",
                Body: "Your account has been approved. You can now accept patient connections.",
                Timestamp: DateTime.UtcNow));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send real-time approval notification to doctor {DoctorId}", doctorId);
        }
    }

    public async Task NotifyPatientOfAcceptance(Guid patientId, Guid doctorId)
    {
        var patient = await _patientRepository.GetByPatientId(patientId);
        if (patient == null)
        {
            _logger.LogWarning("Patient {PatientId} not found for notification", patientId);
            return;
        }

        try
        { // To-do: Consider adding more context to the notification message, such as the doctor's name or any additional details relevant to the acceptance.
            await _persistenceService.CreateNotificationForPatientAsync(
                patientId,
                "Connection Accepted",
                "Your connection request has been accepted by the doctor.",
                NotificationType.ConnectionAccepted);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist notification for patient {PatientId}", patientId);
        }

        try
        {
            await _realTimeService.SendToUserAsync(patient.UserId, new NotificationMessage(
                Title: "Connection Accepted",
                Body: "Your connection request has been accepted by the doctor.",
                Timestamp: DateTime.UtcNow,
                DoctorId: doctorId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send real-time notification to patient {PatientId}", patientId);
        }
    }

    public async Task NotifyPatientOfRejection(Guid patientId, Guid doctorId)
    {
        var patient = await _patientRepository.GetByPatientId(patientId);
        if (patient == null)
        {
            _logger.LogWarning("Patient {PatientId} not found for notification", patientId);
            return;
        }

        try
        {
            await _persistenceService.CreateNotificationForPatientAsync(
                patientId,
                "Connection Rejected",
                "Your connection request has been rejected by the doctor.",
                NotificationType.ConnectionRejected);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to persist notification for patient {PatientId}", patientId);
        }

        try
        {
            await _realTimeService.SendToUserAsync(patient.UserId, new NotificationMessage(
                Title: "Connection Rejected",
                Body: "Your connection request has been rejected by the doctor.",
                Timestamp: DateTime.UtcNow,
                DoctorId: doctorId));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send real-time notification to patient {PatientId}", patientId);
        }
    }
}
