using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace healLink.Application.Queries.Doctor
{
    public class GetPendingDoctorApprovalsQueryValidator : AbstractValidator<GetPendingDoctorApprovalsQuery>
    {
        public GetPendingDoctorApprovalsQueryValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.");
        }
    }
}
