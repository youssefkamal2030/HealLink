using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Profile;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Entities;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    public class UpdateDoctorProfileCommandHandler : IRequestHandler<UpdateDoctorProfileCommand, UpdateProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDoctorProfileCommandHandler(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateProfileResponse> Handle(UpdateDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _profileRepository.GetDoctorByIdAsync(request.DoctorId, cancellationToken);

                if (doctor == null)
                    return new UpdateProfileResponse("Doctor profile not found.", false);

                doctor.UpdatePersonalInfo(request.PersonalInfo);
                doctor.UpdateAddress(request.Address);
                doctor.UpdateProfessionalDetails(request.Specialization, request.CurrentWorkplace);

                if (request.IsAvailableForChat.HasValue)
                    doctor.SetChatAvailability(request.IsAvailableForChat.Value);

                await _profileRepository.UpdateDoctorAsync(doctor, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new UpdateProfileResponse("Doctor profile updated successfully.");
            }
            catch (Exception ex)
            {
                return new UpdateProfileResponse($"Failed to update doctor profile: {ex.Message}", false);
            }
        }
    }
}