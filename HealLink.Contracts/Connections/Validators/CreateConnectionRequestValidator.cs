using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HealLink.Contracts.Connections.Requests;

namespace HealLink.Contracts.Connections.Validators
{
    public class CreateConnectionRequestValidator : AbstractValidator<CreateConnectionRequest>
    {
        public CreateConnectionRequestValidator()
        {
            RuleFor(x => x.DoctorId).NotEmpty().WithMessage("DoctorId is required.");
            RuleFor(x => x.PatientId).NotEmpty().WithMessage("PatientId is required.");
            RuleFor(x => x.DoctorId)
                .NotEqual(x => x.PatientId)
                .WithMessage("DoctorId and PatientId cannot be the same.");
        }
    }
}
