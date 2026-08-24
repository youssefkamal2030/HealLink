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

        private User(string username, string passwordHash, string email, UserRole role)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Role = role;
            Status = AccountStatus.Pending;
            AddDomainEvent(new DomainEvents.UserRegisteredEvent(Id, Email, Role.ToString()));
        }

        /// <summary>
        /// Factory method for registering a new user. Raises <see cref="DomainEvents.UserRegisteredEvent"/>.
        /// </summary>
        public static User Register(string username, string passwordHash, string email, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username cannot be empty.", nameof(username));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));
            if (string.IsNullOrWhiteSpace(passwordHash)) throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));

            return new User(username, passwordHash, email, role);
        }

        /// <summary>
        /// Requests a new OTP for email verification. Revokes any existing active OTP.
        /// </summary>
        /// <returns>A newly generated OTP code.</returns>
        public OTP RequestOTP()
        {
            // Revoke any previously active OTP to ensure only one valid code at a time
            var activeOtp = _otps.FirstOrDefault(o => !o.IsUsed && !o.IsExpired());
            if (activeOtp != null)
            {
                activeOtp.Revoke();
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
            otp.MarkAsUsed();
            UpdateTimestamp();
        }
        public void ConfirmEmail()
        {
            EmailConfirmed = true;
            UpdateTimestamp();
        }
    }
}
