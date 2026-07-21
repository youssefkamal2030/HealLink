using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Common.Models;
using healLink.Application.Queries.Doctor;
using healLink.Application.Repositories;
using HealLink.Contracts.Doctor.Responses;
using MediatR;

namespace healLink.Application.Handlers.Doctor
{
    public class GetPendingDoctorApprovalsQueryHandler : IRequestHandler<GetPendingDoctorApprovalsQuery, Result<PaginatedDoctorsResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public GetPendingDoctorApprovalsQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<PaginatedDoctorsResponse>> Handle(GetPendingDoctorApprovalsQuery request, CancellationToken cancellationToken)
        {
            // Query pending doctors via repository
            var (doctors, totalCount) = await _doctorRepository.GetPendingDoctorsAsync(
                request.Page,
                request.PageSize,
                cancellationToken);

            // Map to DTOs
            var doctorSummaries = doctors.Select(d => new DoctorSummaryResponse(
                d.Id,
                d.UserId,
                d.User.Username,
                d.User.Email,
                d.Specialization,
                d.PracticeLicenseNumber,
                d.SyndicateIdImagePath,
                d.CreatedAt
            )).ToList();

            // Create paginated response
            var result = new PaginatedDoctorsResponse(
                doctorSummaries,
                totalCount,
                request.Page,
                request.PageSize
            );

            return Result<PaginatedDoctorsResponse>.Success(result);
        }
    }
}
