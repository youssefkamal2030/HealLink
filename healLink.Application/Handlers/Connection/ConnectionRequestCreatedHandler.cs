using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using healLink.Application.DTOs;
using healLink.Application.Repositories;
using HealLink.Application.Interfaces;
using HealLink.Domain.DomainEvents;
using MediatR;

namespace healLink.Application.Handlers.Connection
{
    public class ConnectionRequestCreatedHandler : INotificationHandler<ConnectionRequestCreatedEvent>
    {
        private readonly INotificationService _notificationService;
        private readonly IPatientRepository _patientRepository;
        public ConnectionRequestCreatedHandler(INotificationService notificationService, IPatientRepository patientRepository)
        {
            _notificationService = notificationService;
            _patientRepository = patientRepository;
        }
        public async Task Handle(ConnectionRequestCreatedEvent request, CancellationToken cancellationToken)
        {
            var patientName = await _patientRepository.GetPatientNameById(request.PatientId);
            var data = new DoctorConnectionRequestNotificationData(request.RequestId, request.PatientId, patientName);
            await _notificationService.NotifyDoctorOfPendingRequest(
                request.DoctorId,data
               
            );
        }
    }
}
