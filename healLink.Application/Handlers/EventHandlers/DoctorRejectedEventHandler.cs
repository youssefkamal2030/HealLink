using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Adapters;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace healLink.Application.Handlers.EventHandlers
{
    /// <summary>
    /// Handles DoctorRejectedEvent by sending rejection notification to the doctor.
    /// Wraps notification in try-catch to prevent exceptions from breaking the transaction.
    /// </summary>
    public class DoctorRejectedEventHandler : INotificationHandler<DomainEventNotification<DoctorRejectedEvent>>
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<DoctorRejectedEventHandler> _logger;

        public DoctorRejectedEventHandler(
            INotificationService notificationService,
            ILogger<DoctorRejectedEventHandler> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Handle(
            DomainEventNotification<DoctorRejectedEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            try
            {
                await _notificationService.NotifyDoctorOfRejection(
                    domainEvent.DoctorId,
                    domainEvent.Reason);

                _logger.LogInformation(
                    "Successfully sent rejection notification to doctor {DoctorId}",
                    domainEvent.DoctorId);
            }
            catch (Exception ex)
            {
                // Log error but don't throw - notification failure should not break the transaction
                _logger.LogError(
                    ex,
                    "Failed to send rejection notification to doctor {DoctorId}. Reason: {Reason}",
                    domainEvent.DoctorId,
                    domainEvent.Reason);
            }
        }
    }
}
