using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using Microsoft.Extensions.Options;
using HealLink.Infrastructure.Config;

namespace HealLink.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
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

        public async Task SendPasswordResetEmailAsync(string to, string Token)
        {
            var subject = "HealLink Password Reset";
            var body = $"the password reset token is: <br>{Token}<br>. Use this token to reset your password. If you did not request this, ignore this email.";
            await SendEmailAsync(to, subject, body);
        }

        public Task SendVerificationEmailAsync(string to, string verificationLink)
        {
            throw new NotImplementedException();
        }
    }
}
