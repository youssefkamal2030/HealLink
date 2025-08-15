using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, UpdateProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public UpdateProfileCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<UpdateProfileResponse> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _profileRepository.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
                
                if (doctor == null)
                {
                    return new UpdateProfileResponse("Doctor profile not found.", false);
                }

                // Update doctor using domain entity methods
                doctor.UpdatePersonalInfo(request.PersonalInfo);
                doctor.UpdateAddress(request.Address);
                doctor.UpdateProfessionalDetails(
                    request.Specialization,
                    request.CurrentWorkplace,
                    request.Phone
                );
                
                if (request.IsAvailableForChat.HasValue)
                {
                    doctor.SetChatAvailability(request.IsAvailableForChat.Value);
                }

                await _profileRepository.UpdateDoctorAsync(doctor, cancellationToken);

                return new UpdateProfileResponse("Doctor profile updated successfully.");
            }
            catch (Exception ex)
            {
                return new UpdateProfileResponse($"Failed to update doctor profile: {ex.Message}", false);
            }
        }
    }
}