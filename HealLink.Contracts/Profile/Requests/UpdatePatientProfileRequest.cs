namespace HealLink.Contracts.Profile.Requests
{
    public record UpdatePatientProfileRequest(
        string Username,
        string Email
    );
}
