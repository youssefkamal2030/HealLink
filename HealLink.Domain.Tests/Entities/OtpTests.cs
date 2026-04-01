using System;
using HealLink.Domain.Entities;
using Xunit;

namespace HealLink.Domain.Tests.Entities
{
    public class OtpTests
    {
        // ── Constructor ──────────────────────────────────────────────────────

        [Fact]
        public void Constructor_WithValidArgs_SetsProperties()
        {
            var expiry = DateTime.UtcNow.AddMinutes(10);
            var userId = Guid.NewGuid();

            var otp = new OTP("123456", expiry, userId);

            Assert.Equal("123456", otp.Code);
            Assert.Equal(expiry, otp.ExpiryTime);
            Assert.Equal(userId, otp.UserId);
            Assert.False(otp.IsUsed);
        }

        [Fact]
        public void Constructor_WithEmptyCode_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new OTP("", DateTime.UtcNow.AddMinutes(10), Guid.NewGuid()));
        }

        [Fact]
        public void Constructor_WithWhitespaceCode_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new OTP("   ", DateTime.UtcNow.AddMinutes(10), Guid.NewGuid()));
        }

        [Fact]
        public void Constructor_WithPastExpiry_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
                new OTP("123456", DateTime.UtcNow.AddMinutes(-1), Guid.NewGuid()));
        }

        [Fact]
        public void Constructor_WithExactlyNowExpiry_ThrowsArgumentException()
        {
            // expiryTime <= UtcNow should throw
            Assert.Throws<ArgumentException>(() =>
                new OTP("123456", DateTime.UtcNow, Guid.NewGuid()));
        }

        // ── IsExpired ────────────────────────────────────────────────────────

        [Fact]
        public void IsExpired_WhenExpiryInFuture_ReturnsFalse()
        {
            var otp = new OTP("123456", DateTime.UtcNow.AddMinutes(10), Guid.NewGuid());

            Assert.False(otp.IsExpired());
        }

        // ── Invalidate ───────────────────────────────────────────────────────

        [Fact]
        public void Invalidate_WhenValid_SetsIsUsedTrue()
        {
            var otp = new OTP("123456", DateTime.UtcNow.AddMinutes(10), Guid.NewGuid());

            otp.Invalidate();

            Assert.True(otp.IsUsed);
        }

        [Fact]
        public void Invalidate_WhenAlreadyUsed_ThrowsInvalidOperationException()
        {
            var otp = new OTP("123456", DateTime.UtcNow.AddMinutes(10), Guid.NewGuid());
            otp.Invalidate();

            var ex = Assert.Throws<InvalidOperationException>(() => otp.Invalidate());
            Assert.Contains("already used", ex.Message);
        }

        [Fact]
        public void Invalidate_UpdatesTimestamp()
        {
            var otp = new OTP("123456", DateTime.UtcNow.AddMinutes(10), Guid.NewGuid());
            var before = otp.UpdatedAt;
            System.Threading.Thread.Sleep(10);

            otp.Invalidate();

            Assert.True(otp.UpdatedAt > before);
        }

        // ── Entity base ──────────────────────────────────────────────────────

        [Fact]
        public void Constructor_AssignsGuidId()
        {
            var otp = new OTP("123456", DateTime.UtcNow.AddMinutes(10), Guid.NewGuid());

            Assert.NotEqual(Guid.Empty, otp.Id);
        }
    }
}
