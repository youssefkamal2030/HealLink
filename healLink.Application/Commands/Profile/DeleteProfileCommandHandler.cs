using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Commands.Profile
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand, DeleteProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;

        public DeleteProfileCommandHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<DeleteProfileResponse> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _profileRepository.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
                
                if (doctor == null)
                {
                    return new DeleteProfileResponse("Doctor profile not found.", false);
                }

                await _profileRepository.DeleteDoctorAsync(request.DoctorId, cancellationToken);

                return new DeleteProfileResponse("Doctor profile deleted successfully.");
            }
            catch (Exception ex)
            {
                return new DeleteProfileResponse($"Failed to delete doctor profile: {ex.Message}", false);
            }
        }
    }
}