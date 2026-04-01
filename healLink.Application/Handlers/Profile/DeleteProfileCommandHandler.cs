using System;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Commands.Profile;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Profile.Responses;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteDoctorProfileCommand, DeleteDoctorProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProfileCommandHandler(IProfileRepository profileRepository, IUnitOfWork unitOfWork)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteDoctorProfileResponse> Handle(DeleteDoctorProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var doctor = await _profileRepository.GetDoctorByIdAsync(request.DoctorId, cancellationToken);
                if (doctor == null)
                    return new DeleteDoctorProfileResponse("Doctor profile not found.", false);

                if (doctor.UserId != request.AuthenticatedUserId)
                    return new DeleteDoctorProfileResponse("Unauthorized: You can only delete your own profile.", false);

                await _profileRepository.DeleteDoctorAsync(request.DoctorId, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return new DeleteDoctorProfileResponse("Doctor profile deleted successfully.");
            }
            catch (Exception ex)
            {
                return new DeleteDoctorProfileResponse($"Failed to delete doctor profile: {ex.Message}", false);
            }
        }
    }
}