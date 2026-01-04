using FluentValidation;

namespace HealLink.Contracts.Profile.Validators
{
    public class UpdateDoctorProfileRequestValidator : AbstractValidator<UpdateDoctorProfileRequest>
    {
        public UpdateDoctorProfileRequestValidator()
        {
            When(x => !string.IsNullOrEmpty(x.FullName), () =>
            {
                RuleFor(x => x.FullName)
                    .MinimumLength(2)
                    .WithMessage("Full name must be at least 2 characters long");
            });

            When(x => !string.IsNullOrEmpty(x.Gender), () =>
            {
                RuleFor(x => x.Gender)
                    .Must(gender => gender == "Male" || gender == "Female" || gender == "Other")
                    .WithMessage("Gender must be Male, Female, or Other");
            });

            When(x => !string.IsNullOrEmpty(x.Specialization), () =>
            {
                RuleFor(x => x.Specialization)
                    .MinimumLength(2)
                    .WithMessage("Specialization must be at least 2 characters long");
            });

            When(x => !string.IsNullOrEmpty(x.City), () =>
            {
                RuleFor(x => x.City)
                    .MinimumLength(2)
                    .WithMessage("City must be at least 2 characters long");
            });

            When(x => !string.IsNullOrEmpty(x.Country), () =>
            {
                RuleFor(x => x.Country)
                    .MinimumLength(2)
                    .WithMessage("Country must be at least 2 characters long");
            });

            When(x => !string.IsNullOrEmpty(x.CurrentWorkplace), () =>
            {
                RuleFor(x => x.CurrentWorkplace)
                    .MinimumLength(2)
                    .WithMessage("Current workplace must be at least 2 characters long");
            });
        }
    }
}
