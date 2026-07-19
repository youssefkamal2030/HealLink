using FluentValidation;

namespace healLink.Application.Commands.Doctor
{
    public class RejectDoctorCommandValidator : AbstractValidator<RejectDoctorCommand>
    {
        public RejectDoctorCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty()
                .WithMessage("DoctorId must not be empty.");

            RuleFor(x => x.Reason)
                .NotEmpty()
                .WithMessage("Rejection reason must not be empty.")
                .MinimumLength(10)
                .WithMessage("Rejection reason must be at least 10 characters.");

            RuleFor(x => x.AdminId)
                .NotEmpty()
                .WithMessage("AdminId must not be empty.");
        }
    }
}
