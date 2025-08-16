using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Profile;
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
            var skip = (request.Page - 1) * request.PageSize;
            var take = request.PageSize;

            var doctors = await _profileRepository.GetAllDoctorsWithUsersAsync(skip, take, request.SearchTerm, cancellationToken);
            var patients = await _profileRepository.GetAllPatientsWithUsersAsync(skip, take, request.SearchTerm, cancellationToken);
            var doctorsCount = await _profileRepository.GetDoctorsCountAsync(request.SearchTerm, cancellationToken);
            var patientsCount = await _profileRepository.GetPatientsCountAsync(request.SearchTerm, cancellationToken);

            var doctorProfiles = doctors.Select(d => new DoctorProfileResponse(
                Id: d.Id,
                UserId: d.UserId,
                FullName: d.User.Username,
                Email: d.User.Email,
                Specialization: d.Specialization,
                CurrentWorkplace: d.CurrentWorkplace,
                PracticeLicenseNumber: d.PracticeLicenseNumber,
                Address: d.Address != null 
                    ? $"{d.Address.Street}, {d.Address.City}, {d.Address.State}, {d.Address}"
                    : string.Empty,
                IsApproved: d.IsApproved,
                IsAvailableForChat: d.IsAvailableForChat,
                CreatedAt: d.CreatedAt,
                UpdatedAt: d.UpdatedAt
            )).ToList();

            var patientProfiles = patients.Select(p => new PatientProfileResponse(
                Id: p.Id,
                UserId: p.UserId,
                FullName: p.User.Username,
                Email: p.User.Email,
                GuardianId: p.GuardianId,
                GuardianName: p.Guardian?.User != null 
                    ? p.Guardian.User.Username
                    : null,
                CreatedAt: p.CreatedAt,
                UpdatedAt: p.UpdatedAt
            )).ToList();

            var totalCount = doctorsCount + patientsCount;

            return new AllProfilesResponse(
                Success: true,
                Message: $"Retrieved {doctorProfiles.Count} doctors and {patientProfiles.Count} patients successfully",
                Doctors: doctorProfiles,
                Patients: patientProfiles,
                TotalCount: totalCount
            );
        }
    }
}
