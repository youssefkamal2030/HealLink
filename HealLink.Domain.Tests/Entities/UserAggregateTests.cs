using System;
using HealLink.Domain.DomainEvents;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using Xunit;

namespace HealLink.Domain.Tests.Entities
{
    // TODO: [TEST-NEXT] Add tests for UpdateProfile() — verify Username and Email are updated, and null throws
    //   ArgumentNullException for each.
    // TODO: [TEST-NEXT] Add a test for RequestOTP_WhenExistingOtpIsAlreadyUsed_DoesNotThrow — calling RequestOTP
    //   when the previous OTP is already used should not crash (the invalidation guard only applies to active OTPs).
    // TODO: [TEST-NEXT] Add a test for the OTPs collection being read-only — verify that user.OTPs is
    //   IReadOnlyCollection and cannot be cast to List<OTP> from outside.
    // TODO: [TEST-NEXT] Add a test for RecordLogin_UpdatesLastLoginAt — verify LastLoginAt is set and
    //   is >= the time before the call.
    public class UserAggregateTests
    {
        private User CreateUser(UserRole role = UserRole.Patient)
            => new User("testuser", "hashedpassword", "test@example.com", role);

        // ── Constructor ──────────────────────────────────────────────────────

        [Fact]
        public void Constructor_WithValidArgs_SetsProperties()
        {
            var user = CreateUser();

            Assert.Equal("testuser", user.Username);
            Assert.Equal("test@example.com", user.Email);
            Assert.Equal(AccountStatus.Pending, user.Status);
            Assert.False(user.EmailConfirmed);
        }

        [Fact]
        public void Constructor_RaisesUserRegisteredEvent()
        {
            var user = CreateUser();

            Assert.Single(user.DomainEvents);
            var evt = Assert.IsType<UserRegisteredEvent>(user.DomainEvents.First());
            Assert.Equal(user.Id, evt.id);
            Assert.Equal(user.Email, evt.Email);
        }

        [Fact]
        public void Constructor_WithNullUsername_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new User(null, "hash", "email@test.com", UserRole.Patient));
        }

        [Fact]
        public void Constructor_WithNullEmail_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new User("user", "hash", null, UserRole.Patient));
        }

        // ── RequestOTP ───────────────────────────────────────────────────────

        [Fact]
        public void RequestOTP_AddsOtpToCollection()
        {
            var user = CreateUser();

            user.RequestOTP();

            Assert.Single(user.OTPs);
        }

        [Fact]
        public void RequestOTP_WhenActiveOtpExists_InvalidatesOldOneAndAddsNew()
        {
            var user = CreateUser();
            user.RequestOTP();

            user.RequestOTP();

            Assert.Equal(2, user.OTPs.Count);
            var first = user.OTPs.First();
            Assert.True(first.IsUsed);
        }

        [Fact]
        public void RequestOTP_ReturnsNewOtp()
        {
            var user = CreateUser();

            var otp = user.RequestOTP();

            Assert.NotNull(otp);
            Assert.False(string.IsNullOrWhiteSpace(otp.Code));
            Assert.False(otp.IsExpired());
        }

        // ── InvalidateOtp ────────────────────────────────────────────────────

        [Fact]
        public void InvalidateOtp_WithValidCode_MarksOtpAsUsed()
        {
            var user = CreateUser();
            var otp = user.RequestOTP();

            user.InvalidateOtp(otp.Code);

            Assert.True(user.OTPs.First().IsUsed);
        }

        [Fact]
        public void InvalidateOtp_WithNonExistentCode_ThrowsInvalidOperationException()
        {
            var user = CreateUser();

            Assert.Throws<InvalidOperationException>(() => user.InvalidateOtp("000000"));
        }

        [Fact]
        public void InvalidateOtp_WithAlreadyUsedOtp_ThrowsInvalidOperationException()
        {
            var user = CreateUser();
            var otp = user.RequestOTP();
            user.InvalidateOtp(otp.Code);

            Assert.Throws<InvalidOperationException>(() => user.InvalidateOtp(otp.Code));
        }

        // ── ConfirmEmail ─────────────────────────────────────────────────────

        [Fact]
        public void ConfirmEmail_SetsEmailConfirmedTrue()
        {
            var user = CreateUser();

            user.ConfirmEmail();

            Assert.True(user.EmailConfirmed);
        }

        [Fact]
        public void ConfirmEmail_UpdatesTimestamp()
        {
            var user = CreateUser();
            var before = user.UpdatedAt;
            System.Threading.Thread.Sleep(10);

            user.ConfirmEmail();

            Assert.True(user.UpdatedAt > before);
        }

        // ── Activate / Suspend / Deactivate ──────────────────────────────────

        [Fact]
        public void Activate_SetsStatusToActive()
        {
            var user = CreateUser();
            user.Activate();
            Assert.Equal(AccountStatus.Active, user.Status);
        }

        [Fact]
        public void Suspend_SetsStatusToSuspended()
        {
            var user = CreateUser();
            user.Activate();
            user.Suspend();
            Assert.Equal(AccountStatus.Suspended, user.Status);
        }

        [Fact]
        public void Deactivate_SetsStatusToDeactivated()
        {
            var user = CreateUser();
            user.Deactivate();
            Assert.Equal(AccountStatus.Deactivated, user.Status);
        }

        // ── ChangePassword ───────────────────────────────────────────────────

        [Fact]
        public void ChangePassword_WithNullHash_ThrowsArgumentNullException()
        {
            var user = CreateUser();
            Assert.Throws<ArgumentNullException>(() => user.ChangePassword(null));
        }

        [Fact]
        public void ChangePassword_UpdatesPasswordHash()
        {
            var user = CreateUser();
            user.ChangePassword("newhash");
            Assert.Equal("newhash", user.PasswordHash);
        }

        // ── RecordLogin ──────────────────────────────────────────────────────

        [Fact]
        public void RecordLogin_SetsLastLoginAt()
        {
            var user = CreateUser();
            var before = DateTime.UtcNow;

            user.RecordLogin();

            Assert.NotNull(user.LastLoginAt);
            Assert.True(user.LastLoginAt >= before);
        }
    }
}
