using healLink.Application.Common.Models;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Search.Responses;
using MediatR;

namespace healLink.Application.Handlers.Search
{
    public class SearchDoctorsQueryHandler : IRequestHandler<SearchDoctorsQuery, Result<DoctorSearchResponse>>
    {
        private readonly IDoctorRepository _doctorRepository;

        public SearchDoctorsQueryHandler(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<Result<DoctorSearchResponse>> Handle(SearchDoctorsQuery request, CancellationToken cancellationToken)
        {
            // Validate pagination
            if (request.Page < 1)
                return Result<DoctorSearchResponse>.Failure("Page must be greater than 0.");

            if (request.PageSize < 1 || request.PageSize > 100)
                return Result<DoctorSearchResponse>.Failure("PageSize must be between 1 and 100.");

            // Search doctors
            var (doctors, totalCount) = await _doctorRepository.SearchDoctorsAsync(
                request.SearchTerm,
                request.Specialization,
                request.City,
                request.Country,
                request.IsAvailableForChat,
                request.IsApprovedOnly,
                request.Page,
                request.PageSize,
                cancellationToken);

            // Map to DTOs
            var doctorDtos = doctors.Select(d => new DoctorSearchResultDto(
                d.Id,
                d.UserId,
                d.PersonalInfo?.FullName ?? d.User?.Email ?? "Unknown",
                d.User?.Email ?? "Unknown",
                d.Specialization,
                d.CurrentWorkplace,
                d.Address?.City,
                d.Address?.Country,
                d.IsAvailableForChat,
                d.IsApproved,
                d.CreatedAt
            )).ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var response = new DoctorSearchResponse(
                doctorDtos,
                totalCount,
                request.Page,
                request.PageSize,
                totalPages
            );

            return Result<DoctorSearchResponse>.Success(response);
        }
    }
}
