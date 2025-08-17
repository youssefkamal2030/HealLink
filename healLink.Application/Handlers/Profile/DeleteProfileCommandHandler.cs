using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Profile;
using healLink.Application.Repositories;
using HealLink.Contracts.Profile.Responses;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteDoctorProfileCommand, DeleteDoctorProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public DeleteProfileCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<DeleteDoctorProfileResponse> Handle(DeleteDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _profileRepository.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
                
                if (doctor == null)
                {
                    return new DeleteDoctorProfileResponse("Doctor profile not found.", false);
                }

                // Authorization check: Only allow deletion if the authenticated user owns the profile
                if (doctor.UserId != request.AuthenticatedUserId)
                {
                    return new DeleteDoctorProfileResponse("Unauthorized: You can only delete your own profile.", false);
                }

                await _profileRepository.DeleteDoctorAsync(request.DoctorId, cancellationToken);

                return new DeleteDoctorProfileResponse("Doctor profile deleted successfully.");
            }
            catch (Exception ex)
            {
                return new DeleteDoctorProfileResponse($"Failed to delete doctor profile: {ex.Message}", false);
            }
        }
    }
}