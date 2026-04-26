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
    // TODO: [REFACTOR-AUTH] Remove inline authorization check after centralized-authorization-infrastructure is implemented
    // PROBLEM: Handler performs inline authorization check (line 34: doctor.UserId != request.AuthenticatedUserId)
    // FIX: Remove authorization logic from handler
    // APPROACH: Authorization will be handled by AuthorizationBehavior with ResourceOwner policy
    // REASON: Separation of concerns - handler should only contain business logic
    // MIGRATION STEPS:
    //   1. Add [Authorize(AuthorizationPolicies.ResourceOwner)] to DeleteDoctorProfileCommand
    //   2. Remove line 34 (authorization check)
    //   3. Remove AuthenticatedUserId from command
    //   4. Handler will focus only on business logic (delete doctor profile)
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

                _notificationRepository.DeleteByRecipientId(request.DoctorId, RecipientType.Doctor);

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