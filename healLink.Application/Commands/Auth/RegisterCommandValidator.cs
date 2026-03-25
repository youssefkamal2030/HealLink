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
            RuleFor(x => x.username).NotEmpty().MinimumLength(3);
            RuleFor(x => x.email).NotEmpty().EmailAddress();
            RuleFor(x => x.password)
                .Matches("[0-9]").WithMessage("Must contain a digit.");
        }
    }
}
