using FluentValidation;

namespace HealLink.Contracts.Auth
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Password)
         .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
             .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
    .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Role)
                .Must(role => new[] { "Patient", "Doctor", "Admin" }.Contains(role))
                .WithMessage("Role must be Patient, Doctor, or Admin.");
            When(x => x.Role.ToLower() == "doctor", () =>
            {
                RuleFor(x => x.Idfront).NotEmpty().WithMessage("Idfront is required for doctors.");
                RuleFor(x => x.Idback).NotEmpty().WithMessage("Idback is required for doctors.");
            });
           
        }
    }
} 