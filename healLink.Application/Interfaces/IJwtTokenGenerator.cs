using HealLink.Domain.Entities;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(User user);
    Task<string> GenerateRefreshTonkenAsync(User user);
    Task<bool> ValidateTokenAsync(string token);
    bool VerifyPasswordResetHmacCode(String codeBase64Url, out Guid userId);
    string CreatePasswordResetHmacCode(Guid userId);

}