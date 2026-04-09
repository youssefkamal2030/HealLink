using healLink.Application.Commands.Profile;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Profile.Responses;
using HealLink.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace healLink.Application.Handlers.Profile
{
    public class DeleteProfileCommandHandler : IRequestHandler<DeleteDoctorProfileCommand, DeleteDoctorProfileResponse>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepository _notificationRepository;
        public DeleteProfileCommandHandler(IProfileRepository profileRepository, IUnitOfWork unitOfWork, INotificationRepository notificationRepository)
        {
            _profileRepository = profileRepository;
            _unitOfWork = unitOfWork;
            _notificationRepository = notificationRepository;
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

                _notificationRepository.DeleteByRecipient(request.DoctorId, RecipientType.Doctor);

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