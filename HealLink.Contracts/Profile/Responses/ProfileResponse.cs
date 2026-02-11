using System;

namespace HealLink.Contracts.Profile.Responses
{
    public record ProfileResponse(
        bool Success,
        string Message,
        DoctorProfileResponse? DoctorProfile = null,
        PatientProfileResponse? PatientProfile = null
    );
}
