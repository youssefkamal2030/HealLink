using System;
using System.Collections.Generic;

namespace HealLink.Contracts.Profile
{
    public record AllProfilesResponse(
        bool Success,
        string Message,
        List<DoctorProfileResponse> Doctors,
        List<PatientProfileResponse> Patients,
        int TotalCount
    );
}
