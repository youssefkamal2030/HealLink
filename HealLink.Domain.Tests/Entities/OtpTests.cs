using System;
using HealLink.Domain.Entities;
using HealLink.Domain.Enums;
using Xunit;

namespace HealLink.Domain.Tests.Entities
{
    /// <summary>
    /// OTP can only be created via User.RequestOTP() — the constructor is private
    /// and OTP.Generate() is internal. All tests go through the aggregate.
    /// </summary>
    public class OtpTests
    {
        private static OTP CreateOtp()
        {
            var user = User.Register("testuser", "hash", "test@example.com", UserRole.Patient);
            return user.RequestOTP();
        }

        // ── Generation ───────────────────────────────────────────────────────

        [Fact]
        public void RequestOTP_ProducesNonEmptyCode()
        {
            var otp = CreateOtp();
            Assert.False(string.IsNullOrWhiteSpace(otp.Code));
        }

        [Fact]
        public void RequestOTP_ProducesSixDigitCode()
        {
            var otp = CreateOtp();
            Assert.Equal(6, otp.Code.Length);
            Assert.True(int.TryParse(otp.Code, out _));
        }

        [Fact]
        public void RequestOTP_ExpiryIsInFuture()
        {
            var otp = CreateOtp();
            Assert.True(otp.ExpiryTime > DateTime.UtcNow);
        }

        [Fact]
        public void RequestOTP_IsNotUsedOnCreation()
        {
            var otp = CreateOtp();
            Assert.False(otp.IsUsed);
        }

        [Fact]
        public void RequestOTP_AssignsGuidId()
        {
            var otp = CreateOtp();
            Assert.NotEqual(Guid.Empty, otp.Id);
        }

        // ── IsExpired ────────────────────────────────────────────────────────

        [Fact]
        public void IsExpired_WhenJustCreated_ReturnsFalse()
        {
            var otp = CreateOtp();
            Assert.False(otp.IsExpired());
        }

        // ── MarkAsUsed ──────────────────────────────────────────────────────

        [Fact]
        public void MarkAsUsed_WhenValid_SetsIsUsedTrue()
        {
            var otp = CreateOtp();
            otp.MarkAsUsed();
            Assert.True(otp.IsUsed);
        }

        [Fact]
        public void MarkAsUsed_WhenAlreadyUsed_ThrowsInvalidOperationException()
        {
            var otp = CreateOtp();
            otp.MarkAsUsed();

            var ex = Assert.Throws<InvalidOperationException>(() => otp.MarkAsUsed());
            Assert.Contains("already used", ex.Message);
        }

        [Fact]
        public void MarkAsUsed_UpdatesTimestamp()
        {
            var otp = CreateOtp();
            var before = otp.UpdatedAt;
            System.Threading.Thread.Sleep(10);

            otp.MarkAsUsed();

            Assert.True(otp.UpdatedAt > before);
        }

        // ── Revoke ──────────────────────────────────────────────────────────

        [Fact]
        public void Revoke_WhenValid_SetsIsUsedTrue()
        {
            var otp = CreateOtp();
            otp.Revoke();
            Assert.True(otp.IsUsed);
        }

        [Fact]
        public void Revoke_WhenAlreadyRevoked_DoesNotThrow()
        {
            var otp = CreateOtp();
            otp.Revoke();
            
            // Should not throw — Revoke() is idempotent
            otp.Revoke();
            Assert.True(otp.IsUsed);
        }

        [Fact]
        public void Revoke_UpdatesTimestamp()
        {
            var otp = CreateOtp();
            var before = otp.UpdatedAt;
            System.Threading.Thread.Sleep(10);

            otp.Revoke();

            Assert.True(otp.UpdatedAt > before);
        }
    }
}
