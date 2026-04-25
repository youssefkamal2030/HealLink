using FluentValidation;

namespace healLink.Application.Commands.Profile
{
    public class UpdatePatientProfileCommandValidator : AbstractValidator<UpdatePatientProfileCommand>
    {
        public UpdatePatientProfileCommandValidator()
        {
            RuleFor(x => x.PatientId)
                .NotEmpty()
                .WithMessage("Patient ID is required.");

            RuleFor(x => x.AuthenticatedUserId)
                .NotEmpty()
                .WithMessage("Authenticated user ID is required.");

            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50)
                .WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.");
        }
    }
}
