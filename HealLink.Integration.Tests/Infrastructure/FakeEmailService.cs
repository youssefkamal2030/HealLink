using healLink.Application.Interfaces;
using HealLink.Domain.Entities;

namespace HealLink.Integration.Tests.Infrastructure
{
    /// <summary>
    /// No-op email service for integration tests — prevents real SMTP calls.
    /// </summary>
    public class FakeEmailService : IEmailService
    {
        public Task SendEmailAsync(string to, string subject, string body) => Task.CompletedTask;
        public Task SendOtpAsync(User user) => Task.CompletedTask;
        public Task SendPasswordResetOtpAsync(User user) => Task.CompletedTask;
        public Task SendPasswordResetEmailAsync(string to, string token) => Task.CompletedTask;
        public Task SendVerificationEmailAsync(string to, string verificationLink) => Task.CompletedTask;
        public Task ConfirmEmailAsync(HealLink.Contracts.Email.ConfirmEmailRequest request) => Task.CompletedTask;
    }
}
