using FluentValidation;
using HealLink.Contracts.Auth.Requests;

namespace HealLink.Contracts.Auth.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Password)
         
             .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
    .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
   
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