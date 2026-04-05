using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Adapters;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace healLink.Application.Handlers.EventHandlers
{
    public class ConnectionAcceptedEventHandler : INotificationHandler<DomainEventNotification<ConnectionAcceptedEvent>>
    {
        private readonly INotificationService _notificationService;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ConnectionAcceptedEventHandler> _logger;

        public ConnectionAcceptedEventHandler(
            INotificationService notificationService,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            ILogger<ConnectionAcceptedEventHandler> logger)
        {
            _notificationService = notificationService;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Handle(DomainEventNotification<ConnectionAcceptedEvent> domainEvent, CancellationToken cancellationToken)
        {
            // TODO: [REFACTOR-P2] This handler calls _unitOfWork.SaveChangesAsync() from inside an event handler
            // that was already triggered by a SaveChangesAsync(). This is a nested save. It is safe today only
            // because AddConnectedDoctor raises no domain events, so the second save finds nothing to dispatch.
            // The correct fix: replace the direct patient mutation + save here with a dispatched command:
            //   await _mediator.Send(new UpdatePatientConnectionCommand(notification.PatientId, notification.DoctorId, ConnectionOperation.Add), cancellationToken);
            // That command handler owns its own clean SaveChangesAsync with no nesting.
            var notification = domainEvent.DomainEvent; 
            var patient = await _patientRepository.GetByPatientId(notification.PatientId);
            if (patient == null)
            {
                _logger.LogWarning("ConnectionAcceptedHandler: Patient {PatientId} not found — skipping ConnectedDoctorIds update.", notification.PatientId);
            }
            else
            {
                patient.AddConnectedDoctor(notification.DoctorId);
                await _patientRepository.UpdateAsync(patient);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            await _notificationService.NotifyPatientOfAcceptance(
                notification.PatientId,
                notification.DoctorId
            );
        }

     
    }
}
