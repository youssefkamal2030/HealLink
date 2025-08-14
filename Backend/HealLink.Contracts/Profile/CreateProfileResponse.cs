namespace healLink.Application.Commands.Profile
{
    public record CreateProfileResponse(
        string Message,
        bool Success = true
    );
}