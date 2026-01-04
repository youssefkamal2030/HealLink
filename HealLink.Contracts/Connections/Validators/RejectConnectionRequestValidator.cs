using FluentValidation;
using System;

namespace HealLink.Contracts.Connections.Validators
{
    public class RejectConnectionRequestValidator : AbstractValidator<Requests.RejectConnectionRequest>
    {
        public RejectConnectionRequestValidator()
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
