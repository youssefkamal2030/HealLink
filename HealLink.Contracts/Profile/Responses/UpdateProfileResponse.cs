using System;

    public record UpdateProfileResponse(
        string Message,
        bool Success = true
    );