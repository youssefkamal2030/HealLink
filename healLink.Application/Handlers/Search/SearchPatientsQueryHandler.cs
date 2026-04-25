using healLink.Application.Common.Models;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Search.Responses;
using MediatR;

namespace healLink.Application.Handlers.Search
{
    public class SearchPatientsQueryHandler : IRequestHandler<SearchPatientsQuery, Result<PatientSearchResponse>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IGuardianRepository _guardianRepository;

        public SearchPatientsQueryHandler(
            IPatientRepository patientRepository,
            IGuardianRepository guardianRepository)
        {
            _patientRepository = patientRepository;
            _guardianRepository = guardianRepository;
        }

        public async Task<Result<PatientSearchResponse>> Handle(SearchPatientsQuery request, CancellationToken cancellationToken)
        {
            // Validate pagination
            if (request.Page < 1)
                return Result<PatientSearchResponse>.Failure("Page must be greater than 0.");

            if (request.PageSize < 1 || request.PageSize > 100)
                return Result<PatientSearchResponse>.Failure("PageSize must be between 1 and 100.");

            // Search patients
            var (patients, totalCount) = await _patientRepository.SearchPatientsAsync(
                request.SearchTerm,
                request.City,
                request.Country,
                request.HasGuardian,
                request.Page,
                request.PageSize,
                cancellationToken);

            // Map to DTOs
            var patientDtos = new List<PatientSearchResultDto>();
            foreach (var patient in patients)
            {
                string? guardianName = null;
                if (patient.GuardianId.HasValue)
                {
                    var guardian = await _guardianRepository.GetByIdAsync(patient.GuardianId.Value, cancellationToken);
                    guardianName = guardian?.User?.Email; // Or get name from PersonalInfo if available
                }

                patientDtos.Add(new PatientSearchResultDto(
                    patient.Id,
                    patient.UserId,
                    patient.User?.Email ?? "Unknown", // Can be enhanced with PersonalInfo
                    patient.User?.Email ?? "Unknown",
                    patient.GuardianId,
                    guardianName,
                    patient.CreatedAt
                ));
            }

            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);

            var response = new PatientSearchResponse(
                patientDtos,
                totalCount,
                request.Page,
                request.PageSize,
                totalPages
            );

            return Result<PatientSearchResponse>.Success(response);
        }
    }
}
