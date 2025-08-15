using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using healLink.Application.Interfaces;
using Microsoft.Extensions.Options;
using HealLink.Infrastructure.Config;
using HealLink.Infrastructure.Data;
using ErrorOr;
using HealLink.Domain.Entities;
using HealLink.Infrastructure.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using HealLink.Contracts.Email;
using Microsoft.AspNetCore.Identity.Data;
using System.Runtime.Intrinsics.X86;
using healLink.Application.Repositories;

namespace HealLink.Infrastructure.Services
{
    public class EmailService(IOptions<EmailSettings> options, HealLinkDbContext context
        , EmailBodyBuilder builder,
         ILogger<User>logger,
 IEmailSender emailSender,
 IUserRepository userRepository
  ) : IEmailService
    {
        private readonly EmailSettings _settings = options.Value;
        private readonly HealLinkDbContext _context = context;
        private readonly EmailBodyBuilder _builder = builder;
        private readonly ILogger<User> _logger = logger;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly int _otpExpiryMinutes = 5;

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

        public async Task SendOtpAsync(User  user)
        {
            var otpCode = new Random().Next(100000, 999999).ToString();

            var existingOtps = await _context.OTPs
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            if (existingOtps.Any())
            {
                _context.OTPs.RemoveRange(existingOtps);
            }

            var otp = new OTP
            {
                Code = otpCode,
                ExpiryTime = DateTime.UtcNow.AddMinutes(_otpExpiryMinutes),
                UserId = user.Id,
                User = user
            };

            _context.OTPs.Add(otp);
            await _context.SaveChangesAsync();

            var emailBody = _builder.GenerateEmailBody("EmailConfirmation",
                templateModel: new Dictionary<string, string>
                {
            { "{{name}}", user.FirstName },
            { "{{otp_code}}", otpCode },
            { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
                }
            );

            await _emailSender.SendEmailAsync(user.Email!, "✅ Heal Link: Email Verification OTP", emailBody);

            _logger.LogInformation("OTP sent to user {UserId}: {OtpCode}", user.Id, otpCode);

        }


        public async Task<bool> ConfirmEmailAsync(ConfirmEmailRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (user == null)
                return false;

            var otpRecord = await _context.OTPs
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.Code == request.Code);

            if (otpRecord == null || otpRecord.ExpiryTime < DateTime.UtcNow)
                return false;

            user.EmailConfirmed = true;
            _context.OTPs.RemoveRange(_context.OTPs.Where(x => x.UserId == user.Id));
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ResendConfirmationEmailAsync(ResendConfirmationEmailRequest request)
        {
            if (await _context.Users.FirstOrDefaultAsync(x=>x.Email==request.Email) is not { } user)
                throw new InvalidOperationException("User not found");
            await SendOtpAsync(user);

       
        }
        public async Task<bool> SendResetOtpAsync(string email)
        {
            if (await _context.Users.FirstOrDefaultAsync(x=>x.Email==email) is not { } user)
                throw new InvalidOperationException("User not found");

            if (!user.EmailConfirmed)
                throw new InvalidOperationException("Email not Confirmed");

            await SendPasswordResetOtpAsync(user);
            return true;
        }
        public  async Task SendPasswordResetOtpAsync(User user)
        {
            var otpCode = new Random().Next(100000, 999999).ToString();

            var existingOtps = await _context.OTPs
                .Where(x => x.UserId == user.Id)
                .ToListAsync();

            if (existingOtps.Any())
            {
                _context.OTPs.RemoveRange(existingOtps);
            }

            var otp = new OTP
            {
                Code = otpCode,
                ExpiryTime = DateTime.UtcNow.AddMinutes(_otpExpiryMinutes),
                UserId = user.Id,
                User = user
            };

            _context.OTPs.Add(otp);
            await _context.SaveChangesAsync();

            var emailBody = _builder.GenerateEmailBody("ForgetPassword",
                templateModel: new Dictionary<string, string>
                {
                { "{{name}}", user.FirstName },
                { "{{otp_code}}", otpCode },
                { "{{expiry_minutes}}", _otpExpiryMinutes.ToString() }
                }
            );

            await _emailSender.SendEmailAsync(user.Email!, "🔐 FitMe: Password Reset OTP", emailBody);

            _logger.LogInformation("Password reset OTP sent to user {UserId}: {OtpCode}", user.Id, otpCode);

        }

        public async Task ResendConfirmationEmailAsync(string email,CancellationToken cancellationToken=default)
        {
            if (await _userRepository.GetByEmailAsync(email, cancellationToken) is not { } user)
                throw new InvalidOperationException("User not found");
            if (user.EmailConfirmed)
                throw new InvalidOperationException(" Email not Confirmed");

            await SendOtpAsync(user);

        }

    }
}
