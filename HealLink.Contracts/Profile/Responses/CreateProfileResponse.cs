namespace HealLink.Contracts.Profile.Responses
{
    public record CreateProfileResponse(
        string Message,
        bool Success = true
    );
}