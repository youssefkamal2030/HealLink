using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Contracts.Email;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Config;
using HealLink.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HealLink.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly EmailBodyBuilder _builder;
        private readonly ILogger<EmailService> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IUserRepository _userRepository;
        private readonly int _otpExpiryMinutes = 5;

        public EmailService(
            IOptions<EmailSettings> options,
            EmailBodyBuilder builder,
            ILogger<EmailService> logger,
            IEmailSender emailSender,
            IUserRepository userRepository)
        {
            _settings = options.Value;
            _builder = builder;
            _logger = logger;
            _emailSender = emailSender;
            _userRepository = userRepository;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_settings.SmtpServer, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true
            };
            var mail = new MailMessage(_settings.SenderEmail, to, subject, body)
            {
                IsBodyHtml = true
            };
            await client.SendMailAsync(mail);
        }

        public async Task SendPasswordResetEmailAsync(string to, string token)
        {
            var subject = "HealLink Password Reset";
            var body = $"the password reset token is: <br>{token}<br>. Use this token to reset your password. If you did not request this, ignore this email.";
            await SendEmailAsync(to, subject, body);
        }

        public async Task<string> SendOtpAsync(User user)
        {
            var otpCode = new Random().Next(100000, 999999).ToString();
            user.RequestOTP(otpCode, DateTime.UtcNow.AddMinutes(_otpExpiryMinutes));

            var emailBody = _builder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                    { "{{name}}", user.Username },
                    { "{{otp_code}}", otpCode },
                    { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
                });

            await _emailSender.SendEmailAsync(user.Email!, "✅ Heal Link: Email Verification OTP", emailBody);
            _logger.LogInformation("OTP sent to user {UserId}", user.Id);
            return otpCode;
        }

        public async Task<string> SendPasswordResetOtpAsync(User user)
        {
            var otpCode = new Random().Next(100000, 999999).ToString();
            user.RequestOTP(otpCode, DateTime.UtcNow.AddMinutes(_otpExpiryMinutes));

            var emailBody = _builder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                    { "{{name}}", user.Username },
                    { "{{otp_code}}", otpCode },
                    { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
                });

            await _emailSender.SendEmailAsync(user.Email!, "🔐 Heal Link: Password Reset OTP", emailBody);
            _logger.LogInformation("Password reset OTP sent to user {UserId}", user.Id);
            return otpCode;
        }

        public async Task ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            // Superseded by ConfirmEmailCommandHandler — kept for interface compatibility.
            var user = await _userRepository.GetByEmailAsync(request.Email, CancellationToken.None);
            if (user == null)
                throw new InvalidOperationException("User not found");

            if (!user.EmailConfirmed)
                throw new InvalidOperationException("Use POST /Auth/confirm-email with the OTP code to confirm email.");
        }
    }
}
