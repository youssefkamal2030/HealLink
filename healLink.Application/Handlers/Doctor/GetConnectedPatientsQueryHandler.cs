// HealLink.Application/Handlers/Doctor/GetConnectedPatientsQueryHandler.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Doctor.Responses;
using HealLink.Contracts.Profile.Responses;
using HealLink.Domain.Enums;
using MediatR;

namespace HealLink.Application.Handlers.Doctor
{
    public class GetConnectedPatientsQueryHandler : IRequestHandler<GetConnectedPatientsQuery, Result<ConnectedPatientsResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;

        public GetConnectedPatientsQueryHandler(IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }

        public async Task<Result<ConnectedPatientsResponse>> Handle(GetConnectedPatientsQuery request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByDoctorId(request.DoctorId);
            if (doctor == null)
            {
                return Result<ConnectedPatientsResponse>.Failure("doctor not found");
            }

            var connectedPatientIds = doctor.PatientConnections
                .Where(c => c.Status == ConnectionStatus.Accepted)
                .Select(c => c.PatientId)
                .ToList();

            var connectedPatients = new List<PatientProfileResponse>();
            foreach (var patientId in connectedPatientIds)
            {
                var patient = await _patientRepository.GetByPatientId(patientId);
               if (patient?.User != null)
                {
                    connectedPatients.Add(new PatientProfileResponse(
                        Id: patient.Id,
                        UserId: patient.UserId,
                        FullName: patient.User.Username,
                        Email: patient.User.Email,
                        GuardianId: patient.GuardianId
                    ));
                }
            }

            var response = new ConnectedPatientsResponse(
             Success: true,
             Message: "Connected patients retrieved successfully.",
             ConnectedPatients: connectedPatients,
             TotalCount: connectedPatients.Count
         );
            return Result<ConnectedPatientsResponse>.Success(response);
        }
    }
}