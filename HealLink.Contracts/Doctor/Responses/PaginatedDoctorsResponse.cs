using System;
using System.Collections.Generic;

namespace HealLink.Contracts.Doctor.Responses
{
    public record PaginatedDoctorsResponse(
        List<DoctorSummaryResponse> Doctors,
        int TotalCount,
        int Page,
        int PageSize
    );
}
