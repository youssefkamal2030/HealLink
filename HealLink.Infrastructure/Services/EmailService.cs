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
            var mail = new MailMessage(_settings.Username, to, subject, body)
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

        // TODO: [REFACTOR] This method does three things it shouldn't:
        //   1. Generates the OTP code (should be in the handler or a domain service)
        //   2. Mutates the domain aggregate via user.RequestOTP() (infrastructure must not touch domain state)
        //   3. Sends the email (the only thing this service should do)
        //
        // This design is what forced the two-SaveChangesAsync workaround in RegisterCommandHandler.
        // Delete this method and replace with a plain SendOtpEmailAsync(string to, string username, string code, int expiryMinutes).
        // See: IEmailService.SendOtpAsync, RegisterCommandHandler
        public async Task<string> SendOtpEmailAsync(string to, string username, string code, int expiryMinutes)
        {
            
            var emailBody = _builder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
                    { "{{name}}", username },
                    { "{{otp_code}}", code },
                    { "{{expiry_minutes}}", expiryMinutes.ToString() }
                });

            await _emailSender.SendEmailAsync(to, "✅ Heal Link: Email Verification OTP", emailBody);
            _logger.LogInformation("OTP sent to user {Username}", username);
            return code ;
        }

        // TODO: [REFACTOR] Same issue as SendOtpAsync — generates code, mutates aggregate, sends email.
        // Replace with a plain SendPasswordResetEmailAsync(string to, string username, string code, int expiryMinutes).
        public async Task<string> SendPasswordResetEmailAsync(string to, string username, string code, int expiryMinutes)
        {
          
            var emailBody = _builder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                    { "{{name}}",username },
                    { "{{otp_code}}", code },
                    { "{{expiry_minutes}}", expiryMinutes.ToString() }
                });

            await _emailSender.SendEmailAsync(to, "🔐 Heal Link: Password Reset OTP", emailBody);
            _logger.LogInformation("Password reset OTP sent to user {Username}", username);
            return code;
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
