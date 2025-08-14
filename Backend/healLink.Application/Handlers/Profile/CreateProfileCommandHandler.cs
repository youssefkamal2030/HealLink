using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Commands.Profile;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
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
                {
                    return new CreateProfileResponse("Patient profile already exists for this user.", false);
                }

                var newPatient = new Patient(request.UserId);
                await _profileRepository.AddPatientAsync(newPatient, cancellationToken);

                return new CreateProfileResponse("Patient profile created successfully.", true);
            }

            if (request.Role == HealLink.Domain.Enums.UserRole.Doctor)
            {
                var existingDoctor = await _profileRepository.GetDoctorByUserIdAsync(request.UserId, cancellationToken);
                if (existingDoctor != null)
                {
                    return new CreateProfileResponse("Doctor profile already exists for this user.", false);
                }

                // Placeholder: construct Doctor when you have required data (PersonalInfo, Address, etc.)
                // var newDoctor = new Doctor(request.UserId, personalInfo, address, syndicateImagePath, idFront, idBack, licenseNumber, practiceLicenseNumber, specialization, currentWorkplace, phone);
                // await _profileRepository.AddDoctorAsync(newDoctor, cancellationToken);

                return new CreateProfileResponse("Doctor creation requires more data.", false);
            }

            return new CreateProfileResponse("Unsupported role for profile creation.", false);
        }
    }
}
