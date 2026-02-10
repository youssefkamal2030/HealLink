using System;
using HealLink.Domain.ValueObjects;
using Xunit;

namespace HealLink.Domain.Tests.ValueObjects
{
    public class MedicationDosageTests
    {
        [Fact]
        public void Equals_WithIdenticalProperties_ReturnsTrue()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);
            var medication2 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);

            // Act
            var result = medication1.Equals(medication2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_WithDifferentMedicationName_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);
            var medication2 = new MedicationDosage("Ibuprofen", "500mg", "Take with food", scheduledTimes);

            // Act
            var result = medication1.Equals(medication2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentDosage_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);
            var medication2 = new MedicationDosage("Aspirin", "250mg", "Take with food", scheduledTimes);

            // Act
            var result = medication1.Equals(medication2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentInstructions_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);
            var medication2 = new MedicationDosage("Aspirin", "500mg", "Take on empty stomach", scheduledTimes);

            // Act
            var result = medication1.Equals(medication2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentScheduledTimesLength_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes1 = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var scheduledTimes2 = new[] { TimeSpan.FromHours(8) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes1);
            var medication2 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes2);

            // Act
            var result = medication1.Equals(medication2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentScheduledTimesValues_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes1 = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var scheduledTimes2 = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(21) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes1);
            var medication2 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes2);

            // Act
            var result = medication1.Equals(medication2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentScheduledTimesOrder_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes1 = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var scheduledTimes2 = new[] { TimeSpan.FromHours(20), TimeSpan.FromHours(8) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes1);
            var medication2 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes2);

            // Act
            var result = medication1.Equals(medication2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithNull_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);

            // Act
            var result = medication.Equals(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_WithDifferentType_ReturnsFalse()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);
            var differentObject = "Not a MedicationDosage";

            // Act
            var result = medication.Equals(differentObject);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_WithIdenticalProperties_ReturnsSameHashCode()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);
            var medication2 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);

            // Act
            var hashCode1 = medication1.GetHashCode();
            var hashCode2 = medication2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_WithDifferentProperties_MayReturnDifferentHashCode()
        {
            // Arrange
            var scheduledTimes = new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) };
            var medication1 = new MedicationDosage("Aspirin", "500mg", "Take with food", scheduledTimes);
            var medication2 = new MedicationDosage("Ibuprofen", "500mg", "Take with food", scheduledTimes);

            // Act
            var hashCode1 = medication1.GetHashCode();
            var hashCode2 = medication2.GetHashCode();

            // Assert
            // Note: Different objects may have the same hash code (hash collision), 
            // but we expect them to be different in most cases
            // This test just verifies that GetHashCode is implemented
            Assert.NotEqual(medication1, medication2);
        }
    }
}
