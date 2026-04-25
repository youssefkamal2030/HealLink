namespace HealLink.Contracts.Auth.Requests
{
    public record ChangePasswordRequest(
        string CurrentPassword,
        string NewPassword
    );
}
