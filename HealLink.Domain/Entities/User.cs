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
    // TODO: [DDD] User.EmailConfirmed has a public setter — state should only change through a domain method (e.g., ConfirmEmail()).
    // TODO: [DDD] OTPs collection is a public field (not a property) — this bypasses encapsulation entirely; should be a private list exposed as IReadOnlyCollection.
    // TODO: [DDD] User does not extend AggregateRoot — it cannot raise domain events (e.g., UserRegistered, PasswordChanged).
    // TODO: [DDD] Email and Username have no format validation in the constructor — domain invariants (valid email format, non-empty username) should be enforced here.
    // TODO: [AGGREGATE-MISSING] User has no aggregate. User + OTP belong in a UserAggregate that extends AggregateRoot. OTP only exists in the context of a User (email confirmation BR-AUTH-03, password reset BR-AUTH-05). The invariants — OTP expiry (BR-AUTH-06), single-use invalidation, account status transitions (BR-AUTH-07) — all belong in one boundary. Without this aggregate, nothing in the domain enforces these rules.
    // TODO: [TOMORROW-1] Promote User to extend AggregateRoot instead of Entity.
    // TODO: [TOMORROW-1] Replace the public OTPs field with a private List<OTP> _otps and expose it as IReadOnlyCollection<OTP> OTPs.
    // TODO: [TOMORROW-1] Add RequestOtp(string code, DateTime expiry) method — creates a new OTP owned by this aggregate, invalidates any existing active OTP for the same user, and adds it to _otps.
    // TODO: [TOMORROW-1] Add InvalidateOtp(string code) method — finds the matching OTP, calls otp.Invalidate(), and enforces that an expired OTP cannot be used (BR-AUTH-06).
    // TODO: [TOMORROW-1] Add ConfirmEmail() method that sets EmailConfirmed = true and removes the public setter from EmailConfirmed.
    // TODO: [TOMORROW-1] Raise a new UserRegisteredEvent(Id, Email, Role) domain event at the end of the constructor. Create UserRegisteredEvent in HealLink.Domain/DomainEvents/.
    public class User : Entity
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public UserRole Role { get; private set; }
        public AccountStatus Status { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public ICollection<OTP> OTPs = [];
        public bool EmailConfirmed { get; set; }
        private User() { } // For EF

        public User(string username, string passwordHash, string email, UserRole role)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
           
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Role = role;
            Status = AccountStatus.Pending;
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
    }
}
