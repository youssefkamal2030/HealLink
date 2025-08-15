using System;

    public record UpdateDoctorProfileResponse(
        string Message,
        bool Success = true
    );