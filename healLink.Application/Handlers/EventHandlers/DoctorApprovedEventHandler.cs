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
    /// Handles DoctorApprovedEvent by sending approval notification to the doctor.
    /// Wraps notification in try-catch to prevent exceptions from breaking the transaction.
    /// </summary>
    public class DoctorApprovedEventHandler : INotificationHandler<DomainEventNotification<DoctorApprovedEvent>>
    {
        private readonly INotificationService _notificationService;
        private readonly ILogger<DoctorApprovedEventHandler> _logger;

        public DoctorApprovedEventHandler(
            INotificationService notificationService,
            ILogger<DoctorApprovedEventHandler> logger)
        {
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Handle(
            DomainEventNotification<DoctorApprovedEvent> notification,
            CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            try
            {
                await _notificationService.NotifyDoctorOfApproval(domainEvent.DoctorId);

                _logger.LogInformation(
                    "Successfully sent approval notification to doctor {DoctorId}",
                    domainEvent.DoctorId);
            }
            catch (Exception ex)
            {
                // Log error but don't throw - notification failure should not break the transaction
                _logger.LogError(
                    ex,
                    "Failed to send approval notification to doctor {DoctorId}",
                    domainEvent.DoctorId);
            }
        }
    }
}
