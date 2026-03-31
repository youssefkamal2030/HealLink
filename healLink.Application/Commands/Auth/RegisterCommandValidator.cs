using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Commands.Auth
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() {
            // TODO: [TASK-B] Strengthen the password rule — current rule only checks for a digit. Add:
            //   .MinimumLength(8)
            //   .Matches("[A-Z]").WithMessage("Must contain at least one uppercase letter.")
            //   .Matches("[a-z]").WithMessage("Must contain at least one lowercase letter.")
            //   .Matches("[^a-zA-Z0-9]").WithMessage("Must contain at least one special character.")
            RuleFor(x => x.username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.password)
                .Matches("[0-9]").WithMessage("Must contain a digit.");
        }
    }
}
