using FluentValidation;
using System;

namespace HealLink.Contracts.Connections.Validators
{
    public class AcceptConnectionRequestValidator : AbstractValidator<Requests.AcceptConnectionRequest>
    {
        public AcceptConnectionRequestValidator()
        {
            RuleFor(x => x.ConnectionId)
                .NotEmpty()
                .WithMessage("Connection ID is required");

            RuleFor(x => x.DoctorId)
                .NotEmpty()
                .WithMessage("Doctor ID is required");
        }
    }
}
