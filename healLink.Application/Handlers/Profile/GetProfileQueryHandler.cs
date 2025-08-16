using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Queries;
using healLink.Application.Repositories;
using HealLink.Contracts.Profile;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;
        public GetProfileQueryHandler(IProfileRepository profileRepository, IUserRepository userRepository)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
        }

        public async Task<ProfileResponse> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
          
            var patient = await _profileRepository.GetPatientByUserIdAsync(request.UserId, cancellationToken);
            if (patient != null)
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
                var guardianName = patient.GuardianId.HasValue 
                    ? await _profileRepository.GetGuardianNameByIdAsync(patient.GuardianId.Value, cancellationToken)
                    : null;

                var patientProfile = new PatientProfileResponse(
      Id: patient.Id , 
      UserId: patient.UserId,
      FullName: user?.Username, 
      Email: user?.Email,
      GuardianId: patient?.GuardianId,
      GuardianName: guardianName,
      CreatedAt: patient.CreatedAt,
      UpdatedAt: patient.UpdatedAt
  );

                return new ProfileResponse(
                    Success: true,
                    Message: "Patient profile retrieved successfully",
                    PatientProfile: patientProfile
                );
            }

            var doctor = await _profileRepository.GetDoctorByUserIdAsync(request.UserId, cancellationToken);
            if (doctor != null)
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);
                var address = doctor.Address != null 
                    ? $"{doctor.Address.Street}, {doctor.Address.City}, {doctor.Address.State}, {doctor.Address}"
                    : string.Empty;

                var doctorProfile = new DoctorProfileResponse(
                    Id: doctor.Id,
                    UserId: doctor.UserId,
                    FullName: doctor.PersonalInfo?.FullName ?? user.Username,
                    Email: user.Email,
                    Specialization: doctor.Specialization,
                    CurrentWorkplace: doctor.CurrentWorkplace,
                    PracticeLicenseNumber: doctor.PracticeLicenseNumber,
                    Address: address,
                    IsApproved: doctor.IsApproved,
                    IsAvailableForChat: doctor.IsAvailableForChat,
                    CreatedAt: doctor.CreatedAt,
                    UpdatedAt: doctor.UpdatedAt
                );

                return new ProfileResponse(
                    Success: true,
                    Message: "Doctor profile retrieved successfully",
                    DoctorProfile: doctorProfile
                );
            }

            return new ProfileResponse(
                Success: false,
                Message: "Profile not found for the specified user"
            );
        }
    }
}
