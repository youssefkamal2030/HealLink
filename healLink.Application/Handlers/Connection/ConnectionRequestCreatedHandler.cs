using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class ConnectionRequestCreatedHandler : INotificationHandler<ConnectionRequestCreatedEvent>
    {
        private readonly INotificationService _notificationService;
        public ConnectionRequestCreatedHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        public async Task Handle(ConnectionRequestCreatedEvent request, CancellationToken cancellationToken)
        {
            await _notificationService.NotifyDoctorOfPendingRequest(
                request.DoctorId,
                request.PatientId,
                request.RequestId 
            );
        }
    }
}
