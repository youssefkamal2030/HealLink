using System;
using healLink.Application.Common.Models;
using HealLink.Contracts.Prescriptions.Responses;
using MediatR;

namespace healLink.Application.Queries.Prescriptions
{
    public record GetPrescriptionsByPatientQuery(Guid PatientId) : IRequest<Result<PrescriptionsListResponse>>;
    public record GetPrescriptionsByDoctorQuery(Guid DoctorId) : IRequest<Result<PrescriptionsListResponse>>;
}
