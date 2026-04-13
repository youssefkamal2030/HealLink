using ErrorOr;
using HealLink.Contracts.Email;
using HealLink.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace healLink.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        Task SendPasswordResetEmailAsync(string to, string resetLink);
        // TODO: [REFACTOR] Remove SendOtpAsync and SendPasswordResetOtpAsync entirely.
        // These methods violate separation of concerns — an infrastructure service (email) should not
        // receive a domain entity, mutate it (user.RequestOTP), and send an email all in one call.
        // 
        // Replace with: Task SendOtpEmailAsync(string to, string username, string otpCode, int expiryMinutes)
        // OTP generation and user.RequestOTP() belong in the handler, not here.
        // See: RegisterCommandHandler, EmailService.SendOtpAsync
        Task<string> SendOtpEmailAsync(string to, string username, string otpCode, int expiryMinutes);
        Task<string> SendPasswordResetEmailAsync(string to, string username, string otpCode, int expiryMinutes);
        Task ConfirmEmailAsync(ConfirmEmailRequest request);
    }
}
