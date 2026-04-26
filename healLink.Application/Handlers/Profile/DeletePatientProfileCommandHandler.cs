using healLink.Application.Commands.Profile;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Enums;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    // TODO: [REFACTOR-AUTH] Remove inline authorization check after centralized-authorization-infrastructure is implemented
    // PROBLEM: Handler performs inline authorization check (lines 38-44: isOwner || isAdmin)
    // FIX: Remove authorization logic from handler
    // APPROACH: Authorization will be handled by AuthorizationBehavior with ResourceOwnerOrAdmin policy
    // REASON: Separation of concerns - handler should only contain business logic
    // MIGRATION STEPS:
    //   1. Remove lines 38-44 (authorization check)
    //   2. Remove AuthenticatedUserId from command
    //   3. Remove authenticatedUser lookup (lines 33-37)
    //   4. Handler will focus only on business logic (delete patient profile)
    public class DeletePatientProfileCommandHandler : IRequestHandler<DeletePatientProfileCommand, Result<bool>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePatientProfileCommandHandler(
            IPatientRepository patientRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(DeletePatientProfileCommand request, CancellationToken cancellationToken)
        {
            // Get patient
            var patient = await _patientRepository.GetByPatientId(request.PatientId);
            if (patient == null)
                return Result<bool>.Failure("Patient not found.");

            // Get authenticated user to check authorization
            var authenticatedUser = await _userRepository.GetUserByIdAsync(request.AuthenticatedUserId, cancellationToken);
            if (authenticatedUser == null)
                return Result<bool>.Failure("Authenticated user not found.");

            // Authorization: Only the patient themselves or an admin can delete
            bool isOwner = patient.UserId == request.AuthenticatedUserId;
            bool isAdmin = authenticatedUser.Role == UserRole.Admin;

            if (!isOwner && !isAdmin)
                return Result<bool>.Failure("You are not authorized to delete this profile.");

            // Get user associated with patient
            var user = await _userRepository.GetUserByIdAsync(patient.UserId, cancellationToken);
            if (user == null)
                return Result<bool>.Failure("User not found.");

            // Soft delete: Deactivate the user account
            user.Deactivate();

            // Update user
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
