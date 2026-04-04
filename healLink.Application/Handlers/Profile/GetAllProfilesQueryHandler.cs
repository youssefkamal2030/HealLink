using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Profile;
using HealLink.Contracts.Profile.Responses;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    public class GetAllProfilesQueryHandler : IRequestHandler<GetAllProfilesQuery, AllProfilesResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public GetAllProfilesQueryHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<AllProfilesResponse> Handle(GetAllProfilesQuery request, CancellationToken cancellationToken)
        {
            var doctorsCount = await _profileRepository.GetDoctorsCountAsync(request.SearchTerm, cancellationToken);
            var patientsCount = await _profileRepository.GetPatientsCountAsync(request.SearchTerm, cancellationToken);
            var totalCount = doctorsCount + patientsCount;

            // Fetch all matching records then paginate the combined list.
            // This is correct for a mixed-type result set where doctors and patients
            // are interleaved on the same page.
            var allDoctors = await _profileRepository.GetAllDoctorsWithUsersAsync(0, doctorsCount, request.SearchTerm, cancellationToken);
            var allPatients = await _profileRepository.GetAllPatientsWithUsersAsync(0, patientsCount, request.SearchTerm, cancellationToken);

            var doctorProfiles = allDoctors.Select(d => new DoctorProfileResponse(
                Id: d.Id,
                UserId: d.UserId,
                FullName: d.User?.Username ?? string.Empty,
                Email: d.User?.Email ?? string.Empty,
                Gender: d.PersonalInfo?.Gender ?? string.Empty,
                nationality: d.PersonalInfo?.Nationality ?? string.Empty,
                city: d.Address?.City ?? string.Empty,
                country: d.Address?.Country ?? string.Empty,
                Specialization: d.Specialization ?? string.Empty,
                CurrentWorkplace: d.CurrentWorkplace ?? string.Empty,
                PracticeLicenseNumber: d.PracticeLicenseNumber ?? string.Empty,
                Address: d.Address != null ? $"{d.Address.City}, {d.Address.Country}" : string.Empty,
                IsApproved: d.IsApproved,
                IsAvailableForChat: d.IsAvailableForChat,
                CreatedAt: d.CreatedAt,
                UpdatedAt: d.UpdatedAt
            )).ToList();

            var patientProfiles = allPatients.Select(p => new PatientProfileResponse(
                Id: p.Id,
                UserId: p.UserId,
                FullName: p.User?.Username ?? string.Empty,
                Email: p.User?.Email ?? string.Empty,
                GuardianId: p.GuardianId
            )).ToList();

            // Apply pagination to the combined set
            var skip = (request.Page - 1) * request.PageSize;
            var pagedDoctors = doctorProfiles.Skip(skip).Take(request.PageSize).ToList();
            var remaining = request.PageSize - pagedDoctors.Count;
            var patientSkip = Math.Max(0, skip - doctorsCount);
            var pagedPatients = remaining > 0
                ? patientProfiles.Skip(patientSkip).Take(remaining).ToList()
                : new List<PatientProfileResponse>();

            return new AllProfilesResponse(
                Success: true,
                Message: $"Retrieved {pagedDoctors.Count + pagedPatients.Count} profiles (page {request.Page})",
                Doctors: pagedDoctors,
                Patients: pagedPatients,
                TotalCount: totalCount
            );
        }
    }
}
