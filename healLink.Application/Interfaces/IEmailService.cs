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
        Task SendOtpAsync(User user);
        Task SendPasswordResetOtpAsync(User user);
        Task ConfirmEmailAsync(ConfirmEmailRequest request);
    }
}
