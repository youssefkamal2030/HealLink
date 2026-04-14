using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealLink.Domain.Base;
using HealLink.Domain.Enums;
using HealLink.Domain.ValueObjects;

namespace HealLink.Domain.Entities
{
    public class User : AggregateRoot
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }
        public AccountStatus Status { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        private readonly List<OTP> _otps = [];
        public IReadOnlyCollection<OTP> OTPs => _otps.AsReadOnly();
        public bool EmailConfirmed { get; private set; }
        private User() { } // For EF

        public User(string username, string passwordHash, string email, UserRole role)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));

            Email = email ?? throw new ArgumentNullException(nameof(email));
            Role = role;
            Status = AccountStatus.Pending;
            AddDomainEvent(new DomainEvents.UserRegisteredEvent(Id, Email, Role.ToString()));

        }

        public OTP RequestOTP()
        {
            var existingOtp = _otps.FirstOrDefault(o => !o.IsUsed && !o.IsExpired());
            if (existingOtp != null)
            {
                existingOtp.Invalidate();
            }
            var newOtp = OTP.Generate();
            _otps.Add(newOtp);
            UpdateTimestamp();
            return newOtp;
        }
        public void Activate()
        {
            Status = AccountStatus.Active;
            UpdateTimestamp();
        }

        public void Suspend()
        {
            Status = AccountStatus.Suspended;
            UpdateTimestamp();
        }

        public void Deactivate()
        {
            Status = AccountStatus.Deactivated;
            UpdateTimestamp();
        }

        public void RecordLogin()
        {
            LastLoginAt = DateTime.UtcNow;
            UpdateTimestamp();
        }

        public void UpdateProfile(string username, string email)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            UpdateTimestamp();
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash ?? throw new ArgumentNullException(nameof(newPasswordHash));
            UpdateTimestamp();
        }
        public void InvalidateOtp(string code)
        {
            var otp = _otps.FirstOrDefault(o => o.Code == code);
            if (otp == null)
                throw new InvalidOperationException("OTP not found");
            otp.Invalidate();
            UpdateTimestamp();
        }
        public void ConfirmEmail()
        {
            EmailConfirmed = true;
            UpdateTimestamp();
        }
    }
}
