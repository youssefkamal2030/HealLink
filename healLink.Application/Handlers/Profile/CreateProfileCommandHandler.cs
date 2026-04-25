using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Profile;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Profile.Responses;
using HealLink.Domain.Enums;
using DomainEntities = HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, CreateProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public CreateProfileCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<CreateProfileResponse> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            if (request.Role == UserRole.Patient)
            {
                var existingPatient = await _profileRepository.GetPatientByUserIdAsync(request.UserId, cancellationToken);
                if (existingPatient != null)
                    return new CreateProfileResponse("Patient profile already exists for this user.", false);

                var newPatient = DomainEntities.Patient.Register(request.UserId);
                await _profileRepository.AddPatientAsync(newPatient, cancellationToken);
                // NOTE: No SaveChangesAsync here — caller is responsible for the commit.
                return new CreateProfileResponse("Patient profile created successfully.", true);
            }

            if (request.Role == UserRole.Doctor)
            {
                var existingDoctor = await _profileRepository.GetDoctorByUserIdAsync(request.UserId, cancellationToken);
                if (existingDoctor != null)
                    return new CreateProfileResponse("Doctor profile already exists for this user.", false);

                var newDoctor = DomainEntities.Doctor.Register(request.UserId, request.syndicateIdImagePath, request.practiceLicenseNumber, request.specialization);
                await _profileRepository.AddDoctorAsync(newDoctor, cancellationToken);
                // NOTE: No SaveChangesAsync here — caller is responsible for the commit.
                return new CreateProfileResponse("Doctor profile created successfully.", true);
            }

            return new CreateProfileResponse("Unsupported role for profile creation.", false);
        }
    }
}
