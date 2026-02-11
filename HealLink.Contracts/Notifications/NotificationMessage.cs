namespace HealLink.Contracts.Notifications;

/// <summary>
/// Immutable DTO for real-time notification data
/// </summary>
public record NotificationMessage(
    string Title,
    string Body,
    DateTime Timestamp,
    Guid? ConnectionRequestId = null,
    Guid? PatientId = null,
    Guid? DoctorId = null
);
