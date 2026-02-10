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
    }
}
