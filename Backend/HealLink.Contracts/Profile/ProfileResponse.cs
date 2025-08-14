using System;

namespace HealLink.Contracts.Profile
{
    public record ProfileResponse(
        bool Success,
        string Message,
        DoctorProfileResponse? DoctorProfile = null,
        PatientProfileResponse? PatientProfile = null
    );
}
