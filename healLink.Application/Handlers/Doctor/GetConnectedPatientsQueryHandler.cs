// HealLink.Application/Handlers/Doctor/GetConnectedPatientsQueryHandler.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Doctor.Responses;
using HealLink.Contracts.Profile;
using HealLink.Domain.Enums;
using MediatR;

namespace HealLink.Application.Handlers.Doctor
{
    public class GetConnectedPatientsQueryHandler : IRequestHandler<GetConnectedPatientsQuery, ConnectedPatientsResponse>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public GetConnectedPatientsQueryHandler(IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<ConnectedPatientsResponse> Handle(GetConnectedPatientsQuery request, CancellationToken cancellationToken)
        {
            var doctorAggregate = await _doctorRepository.GetAggregateByDoctorId(request.DoctorId);
            if (doctorAggregate == null)
            {
                return new ConnectedPatientsResponse(false, "Doctor not found.", new List<PatientProfileResponse>(), 0);
            }

            var connectedPatientIds = doctorAggregate.Connections
                .Where(c => c.Status == ConnectionStatus.Accepted)
                .Select(c => c.PatientId)
                .ToList();

            var connectedPatients = new List<PatientProfileResponse>();
            foreach (var patientId in connectedPatientIds)
            {
                var patientAggregate = await _patientRepository.GetAggregateByPatientId(patientId);
                if (patientAggregate?.Patient != null && patientAggregate.Patient.User != null)
                {
                    connectedPatients.Add(new PatientProfileResponse(
                        Id: patientAggregate.Patient.Id,
                        UserId: patientAggregate.Patient.UserId,
                        FullName: patientAggregate.Patient.User.Username,
                        Email: patientAggregate.Patient.User.Email,
                        GuardianId: patientAggregate.Patient.GuardianId
                    ));
                }
            }

            return new ConnectedPatientsResponse(true, "Connected patients retrieved successfully.", connectedPatients, connectedPatients.Count);
        }
    }
}