using System;
using System.Collections.Generic;
using HealLink.Domain.Entities;
using HealLink.Domain.ValueObjects;
using Xunit;

namespace HealLink.Domain.Tests.Entities
{
    public class PrescriptionTests
    {
        private MedicationDosage CreateValidMedication(string name = "Aspirin")
        {
            return new MedicationDosage(
                name,
                "500mg",
                "Take with food",
                new[] { TimeSpan.FromHours(8), TimeSpan.FromHours(20) }
            );
        }

        private Prescription CreateValidPrescription()
        {
            var medications = new List<MedicationDosage> { CreateValidMedication() };
            return new Prescription(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test notes",
                medications,
                DateTime.UtcNow.AddDays(30)
            );
        }

        [Fact]
        public void AddMedication_WithNullMedication_ThrowsArgumentNullException()
        {
            // Arrange
            var prescription = CreateValidPrescription();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => prescription.AddMedication(null));
            Assert.Equal("medication", exception.ParamName);
        }

        [Fact]
        public void AddMedication_WithDuplicateMedicationName_ThrowsInvalidOperationException()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            var duplicateMedication = CreateValidMedication("Aspirin"); // Same name as the one in prescription

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => prescription.AddMedication(duplicateMedication));
            Assert.Contains("Aspirin", exception.Message);
            Assert.Contains("already exists", exception.Message);
        }

        [Fact]
        public void AddMedication_WithValidMedication_AddsMedicationToCollection()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            var initialCount = prescription.Medications.Count;
            var newMedication = CreateValidMedication("Ibuprofen");

            // Act
            prescription.AddMedication(newMedication);

            // Assert
            Assert.Equal(initialCount + 1, prescription.Medications.Count);
            Assert.Contains(newMedication, prescription.Medications);
        }

        [Fact]
        public void AddMedication_WithValidMedication_UpdatesTimestamp()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            var originalTimestamp = prescription.UpdatedAt;
            
            // Wait a small amount to ensure timestamp difference
            System.Threading.Thread.Sleep(10);
            
            var newMedication = CreateValidMedication("Ibuprofen");

            // Act
            prescription.AddMedication(newMedication);

            // Assert
            Assert.True(prescription.UpdatedAt > originalTimestamp, 
                $"Expected UpdatedAt ({prescription.UpdatedAt}) to be greater than original timestamp ({originalTimestamp})");
        }

        [Fact]
        public void RemoveMedication_WithNullMedication_ThrowsArgumentNullException()
        {
            // Arrange
            var prescription = CreateValidPrescription();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => prescription.RemoveMedication(null));
            Assert.Equal("medication", exception.ParamName);
        }

        [Fact]
        public void RemoveMedication_WithNonExistentMedication_ThrowsInvalidOperationException()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            var nonExistentMedication = CreateValidMedication("NonExistent");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => prescription.RemoveMedication(nonExistentMedication));
            Assert.Contains("not found in prescription", exception.Message);
        }

        [Fact]
        public void RemoveMedication_WhenOnlyOneMedicationExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var medication = CreateValidMedication();
            var medications = new List<MedicationDosage> { medication };
            var prescription = new Prescription(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test notes",
                medications,
                DateTime.UtcNow.AddDays(30)
            );

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => prescription.RemoveMedication(medication));
            Assert.Contains("Cannot remove the last medication", exception.Message);
        }

        [Fact]
        public void RemoveMedication_WithValidMedication_RemovesMedicationFromCollection()
        {
            // Arrange
            var medication1 = CreateValidMedication("Aspirin");
            var medication2 = CreateValidMedication("Ibuprofen");
            var medications = new List<MedicationDosage> { medication1, medication2 };
            var prescription = new Prescription(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test notes",
                medications,
                DateTime.UtcNow.AddDays(30)
            );
            var initialCount = prescription.Medications.Count;

            // Act
            prescription.RemoveMedication(medication1);

            // Assert
            Assert.Equal(initialCount - 1, prescription.Medications.Count);
            Assert.DoesNotContain(medication1, prescription.Medications);
            Assert.Contains(medication2, prescription.Medications);
        }

        [Fact]
        public void RemoveMedication_WithValidMedication_UpdatesTimestamp()
        {
            // Arrange
            var medication1 = CreateValidMedication("Aspirin");
            var medication2 = CreateValidMedication("Ibuprofen");
            var medications = new List<MedicationDosage> { medication1, medication2 };
            var prescription = new Prescription(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test notes",
                medications,
                DateTime.UtcNow.AddDays(30)
            );
            var originalTimestamp = prescription.UpdatedAt;
            
            // Wait a small amount to ensure timestamp difference
            System.Threading.Thread.Sleep(10);

            // Act
            prescription.RemoveMedication(medication1);

            // Assert
            Assert.True(prescription.UpdatedAt > originalTimestamp, 
                $"Expected UpdatedAt ({prescription.UpdatedAt}) to be greater than original timestamp ({originalTimestamp})");
        }

        [Fact]
        public void UpdateMedication_WithNullMedication_ThrowsArgumentNullException()
        {
            // Arrange
            var prescription = CreateValidPrescription();

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => prescription.UpdateMedication(null));
            Assert.Equal("medication", exception.ParamName);
        }

        [Fact]
        public void UpdateMedication_WithNonExistentMedication_ThrowsInvalidOperationException()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            var nonExistentMedication = CreateValidMedication("NonExistent");

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => prescription.UpdateMedication(nonExistentMedication));
            Assert.Contains("not found in prescription", exception.Message);
            Assert.Contains("NonExistent", exception.Message);
        }

        [Fact]
        public void UpdateMedication_WithValidMedication_ReplacesExistingMedication()
        {
            // Arrange
            var originalMedication = CreateValidMedication("Aspirin");
            var medications = new List<MedicationDosage> { originalMedication };
            var prescription = new Prescription(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Test notes",
                medications,
                DateTime.UtcNow.AddDays(30)
            );
            
            var updatedMedication = new MedicationDosage(
                "Aspirin", // Same name
                "1000mg", // Different dosage
                "Take after meals", // Different instructions
                new[] { TimeSpan.FromHours(9), TimeSpan.FromHours(21) } // Different times
            );

            // Act
            prescription.UpdateMedication(updatedMedication);

            // Assert
            Assert.Equal(1, prescription.Medications.Count); // Count should remain the same
            Assert.Contains(updatedMedication, prescription.Medications);
            Assert.DoesNotContain(originalMedication, prescription.Medications);
        }

        [Fact]
        public void UpdateMedication_WithValidMedication_UpdatesTimestamp()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            var originalTimestamp = prescription.UpdatedAt;
            
            // Wait a small amount to ensure timestamp difference
            System.Threading.Thread.Sleep(10);
            
            var updatedMedication = new MedicationDosage(
                "Aspirin", // Same name as existing
                "1000mg",
                "Take after meals",
                new[] { TimeSpan.FromHours(9) }
            );

            // Act
            prescription.UpdateMedication(updatedMedication);

            // Assert
            Assert.True(prescription.UpdatedAt > originalTimestamp, 
                $"Expected UpdatedAt ({prescription.UpdatedAt}) to be greater than original timestamp ({originalTimestamp})");
        }

        [Fact]
        public void Activate_WhenStatusIsExpired_ThrowsInvalidOperationException()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            prescription.Expire(); // Set status to Expired

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => prescription.Activate());
            Assert.Contains("Cannot activate an expired prescription", exception.Message);
        }

        [Fact]
        public void Activate_WhenStatusIsInactive_SetsStatusToActive()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            prescription.Deactivate(); // Set status to Inactive

            // Act
            prescription.Activate();

            // Assert
            Assert.Equal(HealLink.Domain.Enums.PrescriptionStatus.Active, prescription.Status);
        }

        [Fact]
        public void Activate_WhenStatusIsActive_RemainsActive()
        {
            // Arrange
            var prescription = CreateValidPrescription(); // Status is Active by default

            // Act
            prescription.Activate();

            // Assert
            Assert.Equal(HealLink.Domain.Enums.PrescriptionStatus.Active, prescription.Status);
        }

        [Fact]
        public void Activate_UpdatesTimestamp()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            prescription.Deactivate(); // Set to Inactive first
            var originalTimestamp = prescription.UpdatedAt;
            
            // Wait a small amount to ensure timestamp difference
            System.Threading.Thread.Sleep(10);

            // Act
            prescription.Activate();

            // Assert
            Assert.True(prescription.UpdatedAt > originalTimestamp, 
                $"Expected UpdatedAt ({prescription.UpdatedAt}) to be greater than original timestamp ({originalTimestamp})");
        }

        [Fact]
        public void Deactivate_WhenStatusIsExpired_ThrowsInvalidOperationException()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            prescription.Expire(); // Set status to Expired

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => prescription.Deactivate());
            Assert.Contains("Cannot deactivate an expired prescription", exception.Message);
        }

        [Fact]
        public void Deactivate_WhenStatusIsActive_SetsStatusToInactive()
        {
            // Arrange
            var prescription = CreateValidPrescription(); // Status is Active by default

            // Act
            prescription.Deactivate();

            // Assert
            Assert.Equal(HealLink.Domain.Enums.PrescriptionStatus.Inactive, prescription.Status);
        }

        [Fact]
        public void Deactivate_WhenStatusIsInactive_RemainsInactive()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            prescription.Deactivate(); // Set status to Inactive

            // Act
            prescription.Deactivate();

            // Assert
            Assert.Equal(HealLink.Domain.Enums.PrescriptionStatus.Inactive, prescription.Status);
        }

        [Fact]
        public void Deactivate_UpdatesTimestamp()
        {
            // Arrange
            var prescription = CreateValidPrescription(); // Status is Active by default
            var originalTimestamp = prescription.UpdatedAt;
            
            // Wait a small amount to ensure timestamp difference
            System.Threading.Thread.Sleep(10);

            // Act
            prescription.Deactivate();

            // Assert
            Assert.True(prescription.UpdatedAt > originalTimestamp, 
                $"Expected UpdatedAt ({prescription.UpdatedAt}) to be greater than original timestamp ({originalTimestamp})");
        }

        [Fact]
        public void Expire_WhenStatusIsAlreadyExpired_ThrowsInvalidOperationException()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            prescription.Expire(); // Set status to Expired

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => prescription.Expire());
            Assert.Contains("Prescription is already expired", exception.Message);
        }

        [Fact]
        public void Expire_WhenStatusIsActive_SetsStatusToExpired()
        {
            // Arrange
            var prescription = CreateValidPrescription(); // Status is Active by default

            // Act
            prescription.Expire();

            // Assert
            Assert.Equal(HealLink.Domain.Enums.PrescriptionStatus.Expired, prescription.Status);
        }

        [Fact]
        public void Expire_WhenStatusIsInactive_SetsStatusToExpired()
        {
            // Arrange
            var prescription = CreateValidPrescription();
            prescription.Deactivate(); // Set status to Inactive

            // Act
            prescription.Expire();

            // Assert
            Assert.Equal(HealLink.Domain.Enums.PrescriptionStatus.Expired, prescription.Status);
        }

        [Fact]
        public void Expire_UpdatesTimestamp()
        {
            // Arrange
            var prescription = CreateValidPrescription(); // Status is Active by default
            var originalTimestamp = prescription.UpdatedAt;
            
            // Wait a small amount to ensure timestamp difference
            System.Threading.Thread.Sleep(10);

            // Act
            prescription.Expire();

            // Assert
            Assert.True(prescription.UpdatedAt > originalTimestamp, 
                $"Expected UpdatedAt ({prescription.UpdatedAt}) to be greater than original timestamp ({originalTimestamp})");
        }
    }
}
