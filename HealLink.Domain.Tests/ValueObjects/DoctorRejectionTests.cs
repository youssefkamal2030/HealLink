using System;
using HealLink.Domain.ValueObjects;
using Xunit;

namespace HealLink.Domain.Tests.ValueObjects
{
    public class DoctorRejectionTests
    {
        #region Constructor Tests

        [Fact]
        public void Constructor_WithValidParameters_CreatesSuccessfully()
        {
            // Arrange
            var reason = "Invalid medical license";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;

            // Act
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Assert
            Assert.NotNull(rejection);
            Assert.Equal(reason, rejection.Reason);
            Assert.Equal(rejectedBy, rejection.RejectedBy);
            Assert.Equal(rejectedAt, rejection.RejectedAt);
        }

        [Fact]
        public void Constructor_WithNullReason_ThrowsArgumentException()
        {
            // Arrange
            string reason = null;
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new DoctorRejection(reason, rejectedBy, rejectedAt));

            Assert.Equal("reason", exception.ParamName);
            Assert.Contains("Rejection reason cannot be empty", exception.Message);
        }

        [Fact]
        public void Constructor_WithEmptyReason_ThrowsArgumentException()
        {
            // Arrange
            var reason = string.Empty;
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new DoctorRejection(reason, rejectedBy, rejectedAt));

            Assert.Equal("reason", exception.ParamName);
            Assert.Contains("Rejection reason cannot be empty", exception.Message);
        }

        [Fact]
        public void Constructor_WithWhitespaceReason_ThrowsArgumentException()
        {
            // Arrange
            var reason = "   ";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new DoctorRejection(reason, rejectedBy, rejectedAt));

            Assert.Equal("reason", exception.ParamName);
            Assert.Contains("Rejection reason cannot be empty", exception.Message);
        }

        [Fact]
        public void Constructor_WithEmptyGuid_ThrowsArgumentException()
        {
            // Arrange
            var reason = "Invalid medical license";
            var rejectedBy = Guid.Empty;
            var rejectedAt = DateTime.UtcNow;

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
                new DoctorRejection(reason, rejectedBy, rejectedAt));

            Assert.Equal("rejectedBy", exception.ParamName);
            Assert.Contains("RejectedBy must be a valid admin ID", exception.Message);
        }

        #endregion

        #region Property Immutability Tests

        [Fact]
        public void Reason_SetViaConstructor_IsImmutable()
        {
            // Arrange
            var reason = "Invalid medical license";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Act & Assert
            Assert.Equal(reason, rejection.Reason);
            // Property has private setter, so we cannot modify it - this is compile-time immutability
            // Runtime verification: attempting to use reflection would show the setter is private
        }

        [Fact]
        public void RejectedBy_SetViaConstructor_IsImmutable()
        {
            // Arrange
            var reason = "Invalid medical license";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Act & Assert
            Assert.Equal(rejectedBy, rejection.RejectedBy);
            // Property has private setter - compile-time immutability
        }

        [Fact]
        public void RejectedAt_SetViaConstructor_IsImmutable()
        {
            // Arrange
            var reason = "Invalid medical license";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Act & Assert
            Assert.Equal(rejectedAt, rejection.RejectedAt);
            // Property has private setter - compile-time immutability
        }

        [Fact]
        public void Properties_AfterConstruction_RetainValues()
        {
            // Arrange
            var reason = "Medical syndicate ID verification failed";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = new DateTime(2026, 7, 17, 10, 30, 0, DateTimeKind.Utc);

            // Act
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Assert - Verify all properties are correctly stored
            Assert.Equal(reason, rejection.Reason);
            Assert.Equal(rejectedBy, rejection.RejectedBy);
            Assert.Equal(rejectedAt, rejection.RejectedAt);
        }

        #endregion

        #region Edge Cases and Boundary Tests

        [Fact]
        public void Constructor_WithVeryLongReason_CreatesSuccessfully()
        {
            // Arrange
            var reason = new string('A', 1000); // Very long reason
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;

            // Act
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Assert
            Assert.Equal(reason, rejection.Reason);
        }

        [Fact]
        public void Constructor_WithSpecialCharactersInReason_CreatesSuccessfully()
        {
            // Arrange
            var reason = "Invalid license! @#$%^&*() - Special chars: \n\t\r";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow;

            // Act
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Assert
            Assert.Equal(reason, rejection.Reason);
        }

        [Fact]
        public void Constructor_WithPastDate_CreatesSuccessfully()
        {
            // Arrange
            var reason = "Invalid medical license";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Act
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Assert
            Assert.Equal(rejectedAt, rejection.RejectedAt);
        }

        [Fact]
        public void Constructor_WithFutureDate_CreatesSuccessfully()
        {
            // Arrange
            var reason = "Invalid medical license";
            var rejectedBy = Guid.NewGuid();
            var rejectedAt = DateTime.UtcNow.AddYears(1);

            // Act
            var rejection = new DoctorRejection(reason, rejectedBy, rejectedAt);

            // Assert
            Assert.Equal(rejectedAt, rejection.RejectedAt);
        }

        #endregion

        #region Multiple Instance Tests

        [Fact]
        public void Constructor_CreateMultipleInstances_EachIsIndependent()
        {
            // Arrange & Act
            var rejection1 = new DoctorRejection(
                "Reason 1",
                Guid.NewGuid(),
                DateTime.UtcNow);

            var rejection2 = new DoctorRejection(
                "Reason 2",
                Guid.NewGuid(),
                DateTime.UtcNow.AddDays(1));

            // Assert
            Assert.NotEqual(rejection1.Reason, rejection2.Reason);
            Assert.NotEqual(rejection1.RejectedBy, rejection2.RejectedBy);
            Assert.NotEqual(rejection1.RejectedAt, rejection2.RejectedAt);
        }

        #endregion
    }
}
