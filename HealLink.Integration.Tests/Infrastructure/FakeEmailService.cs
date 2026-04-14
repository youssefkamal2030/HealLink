using healLink.Application.Interfaces;
using HealLink.Contracts.Email;

namespace HealLink.Integration.Tests.Infrastructure
{
    /// <summary>
    /// Test double for IEmailService.
    /// SendOtpEmailAsync returns a fixed predictable code so integration tests
    /// can confirm email using FakeEmailService.TestOtpCode without real SMTP.
    ///
    /// OTP generation and user.RequestOTP() now happen in RegisterCommandHandler
    /// before the email is sent — this fake just captures the code passed to it.
    /// </summary>
    public class FakeEmailService : IEmailService
    {
        public const string TestOtpCode = "000000";

        // Stores the last OTP code sent so tests can retrieve it if needed
        public string? LastOtpCode { get; private set; }

        public Task SendEmailAsync(string to, string subject, string body) => Task.CompletedTask;

        public Task SendPasswordResetEmailAsync(string to, string resetLink) => Task.CompletedTask;

        public Task<string> SendOtpEmailAsync(string to, string username, string otpCode, int expiryMinutes)
        {
            LastOtpCode = otpCode;
            return Task.FromResult(otpCode);
        }

        public Task<string> SendPasswordResetEmailAsync(string to, string username, string otpCode, int expiryMinutes)
        {
            LastOtpCode = otpCode;
            return Task.FromResult(otpCode);
        }

        public Task ConfirmEmailAsync(ConfirmEmailRequest request) => Task.CompletedTask;
    }
}
