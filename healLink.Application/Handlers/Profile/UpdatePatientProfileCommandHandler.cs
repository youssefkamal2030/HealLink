using healLink.Application.Commands.Profile;
using healLink.Application.Common.Models;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using MediatR;

namespace healLink.Application.Handlers.Profile
{
    public class UpdatePatientProfileCommandHandler : IRequestHandler<UpdatePatientProfileCommand, Result<bool>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePatientProfileCommandHandler(
            IPatientRepository patientRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(UpdatePatientProfileCommand request, CancellationToken cancellationToken)
        {
            // Get patient
            var patient = await _patientRepository.GetByPatientId(request.PatientId);
            if (patient == null)
                return Result<bool>.Failure("Patient not found.");

            // Authorization: Only the patient themselves can update their profile
            if (patient.UserId != request.AuthenticatedUserId)
                return Result<bool>.Failure("You are not authorized to update this profile.");

            // Get user
            var user = await _userRepository.GetUserByIdAsync(patient.UserId, cancellationToken);
            if (user == null)
                return Result<bool>.Failure("User not found.");

            // Check if email is already taken by another user
            if (user.Email != request.Email)
            {
                var existingUser = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
                if (existingUser != null && existingUser.Id != user.Id)
                    return Result<bool>.Failure("Email is already taken by another user.");
            }

            // Update user profile
            user.UpdateProfile(request.Username, request.Email);

            // Save changes
            await _userRepository.UpdateAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
