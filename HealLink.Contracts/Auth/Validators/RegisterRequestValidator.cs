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
                .Must(role => new[] { "Patient", "Doctor"}.Contains(role))
                .WithMessage("Role must be Patient, Doctor");
            When(x => x.Role == "Doctor", () =>
            {
                RuleFor(x => x.SyndicateId).NotEmpty().WithMessage("SyndicatId is required for doctors.");
            });
           
        }
    }
}