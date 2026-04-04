using healLink.Application.Interfaces;
using HealLink.Domain.Entities;

namespace HealLink.Integration.Tests.Infrastructure
{
    /// <summary>
    /// Test double for IEmailService.
    /// SendOtpAsync calls user.RequestOTP() so the OTP exists in the aggregate
    /// and tests can confirm email without real SMTP.
    /// The OTP code is always "000000" for predictability in tests.
    /// </summary>
    public class FakeEmailService : IEmailService
    {
        public const string TestOtpCode = "000000";

        public Task SendEmailAsync(string to, string subject, string body) => Task.CompletedTask;

        public Task<string> SendOtpAsync(User user)
        {
            user.RequestOTP(TestOtpCode, DateTime.UtcNow.AddMinutes(10));
            return Task.FromResult(TestOtpCode);
        }

        public Task<string> SendPasswordResetOtpAsync(User user)
        {
            user.RequestOTP(TestOtpCode, DateTime.UtcNow.AddMinutes(10));
            return Task.FromResult(TestOtpCode);
        }

        public Task SendPasswordResetEmailAsync(string to, string token) => Task.CompletedTask;
        public Task SendVerificationEmailAsync(string to, string verificationLink) => Task.CompletedTask;
        public Task ConfirmEmailAsync(HealLink.Contracts.Email.ConfirmEmailRequest request) => Task.CompletedTask;
    }
}
