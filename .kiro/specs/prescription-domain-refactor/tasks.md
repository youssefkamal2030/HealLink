# Implementation Plan: Prescription Domain Refactor

## Overview

This implementation plan refactors the Prescription domain model to eliminate redundancy between MedicationDosage and DosageDetails value objects, add comprehensive business validations, and improve API clarity. The refactoring will be done incrementally, starting with the MedicationDosage consolidation, then updating Prescription entity validations, and finally removing the obsolete DosageDetails class.

## Tasks

- [ ] 1. Refactor MedicationDosage value object
  - [x] 1.1 Add new properties to MedicationDosage
    - Add `Dosage` (string) property with private setter
    - Add `Instructions` (string) property with private setter
    - Keep existing `MedicationName` and `ScheduledTimes` properties
    - _Requirements: 1.1_

  - [x] 1.2 Update MedicationDosage constructor
    - Modify constructor to accept medicationName, dosage, instructions, and scheduledTimes parameters
    - Add validation: throw ArgumentException if medicationName is null, empty, or whitespace
    - Add validation: throw ArgumentException if dosage is null, empty, or whitespace
    - Add validation: throw ArgumentException if instructions is null, empty, or whitespace
    - Add validation: throw ArgumentException if scheduledTimes is null or empty array
    - Remove dependency on DosageDetails parameter
    - _Requirements: 1.3, 1.4, 1.5, 1.6, 1.7_

  - [ ]* 1.3 Write unit tests for MedicationDosage validation
    - Test null parameter rejection for each parameter
    - Test empty string rejection for MedicationName, Dosage, Instructions
    - Test whitespace string rejection for MedicationName, Dosage, Instructions
    - Test null and empty array rejection for ScheduledTimes
    - _Requirements: 1.3, 1.4, 1.5, 1.6, 1.7_

  - [x] 1.4 Implement Equals and GetHashCode for MedicationDosage
    - Override Equals method to compare all properties (MedicationName, Dosage, Instructions, ScheduledTimes)
    - Compare ScheduledTimes arrays element-by-element considering order
    - Override GetHashCode using HashCode.Combine for all properties
    - _Requirements: 8.1, 8.2, 8.3, 8.4, 8.5, 8.6_

  - [ ]* 1.5 Write property test for MedicationDosage equality
    - **Property 5: MedicationDosage Equality Semantics**
    - **Validates: Requirements 8.3, 8.4, 8.5, 8.6**
    - Generate random MedicationDosage instances
    - Test that two instances with identical properties are equal
    - Test that instances with different properties are not equal
    - Test that ScheduledTimes order matters for equality
    - _Requirements: 8.3, 8.4, 8.5, 8.6_

- [ ] 2. Update Prescription constructor and validation
  - [x] 2.1 Refactor Prescription constructor
    - Remove unused `instructions` parameter
    - Keep parameters: patientId, doctorId, notes, medications, expiresAt
    - Add validation: throw InvalidOperationException if medications is null or empty
    - Add validation: throw ArgumentException if expiresAt is in the past
    - Handle null notes by defaulting to empty string
    - _Requirements: 2.1, 2.2, 2.3, 2.4, 3.1, 4.1, 4.3_

  - [ ]* 2.2 Write unit tests for Prescription constructor validation
    - Test empty medications list throws InvalidOperationException
    - Test null medications list throws InvalidOperationException
    - Test past expiration date throws ArgumentException
    - Test null notes defaults to empty string
    - Test null expiresAt is allowed
    - _Requirements: 2.3, 2.4, 3.1, 4.1, 4.3_

- [ ] 3. Implement medication management methods with validation
  - [x] 3.1 Refactor AddMedication method
    - Add validation: throw ArgumentNullException if medication is null
    - Add validation: throw InvalidOperationException if medication with same MedicationName already exists
    - Add medication to collection
    - Call UpdateTimestamp()
    - _Requirements: 7.1, 7.2, 10.1_

  - [~] 3.2 Refactor RemoveMedication method
    - Add validation: throw ArgumentNullException if medication is null
    - Add validation: throw InvalidOperationException if medication doesn't exist in collection
    - Add validation: throw InvalidOperationException if removing would result in zero medications
    - Remove medication from collection
    - Call UpdateTimestamp()
    - _Requirements: 3.2, 3.3, 7.3, 7.4, 10.2_

  - [~] 3.3 Rename and refactor UpdateInstructions to UpdateMedication
    - Rename method from UpdateInstructions to UpdateMedication
    - Add validation: throw ArgumentNullException if medication is null
    - Add validation: throw InvalidOperationException if medication with matching MedicationName doesn't exist
    - Replace existing medication with new instance
    - Call UpdateTimestamp()
    - _Requirements: 6.1, 6.2, 6.3, 6.4, 10.3_

  - [ ]* 3.4 Write unit tests for medication management
    - Test AddMedication with null throws ArgumentNullException
    - Test AddMedication with duplicate throws InvalidOperationException
    - Test RemoveMedication with null throws ArgumentNullException
    - Test RemoveMedication with non-existent medication throws InvalidOperationException
    - Test UpdateMedication with null throws ArgumentNullException
    - Test UpdateMedication with non-existent medication throws InvalidOperationException
    - _Requirements: 7.1, 7.2, 7.3, 7.4, 6.3_

  - [ ]* 3.5 Write property test for last medication protection
    - **Property 2: Last Medication Cannot Be Removed**
    - **Validates: Requirements 3.2, 3.3**
    - Generate prescriptions with exactly one medication
    - Verify RemoveMedication throws InvalidOperationException
    - Verify prescription remains unchanged after exception
    - _Requirements: 3.2, 3.3_

  - [ ]* 3.6 Write property test for medication replacement
    - **Property 4: Update Medication Replaces Existing**
    - **Validates: Requirements 6.4**
    - Generate prescriptions with multiple medications
    - Update one medication by MedicationName
    - Verify old medication is replaced with new one
    - Verify medication count remains the same
    - _Requirements: 6.4_

- [~] 4. Checkpoint - Ensure medication management tests pass
  - Ensure all tests pass, ask the user if questions arise.

- [ ] 5. Implement status transition validation
  - [~] 5.1 Add validation to Activate method
    - Add check: throw InvalidOperationException if current status is Expired
    - Set status to Active
    - Call UpdateTimestamp()
    - _Requirements: 5.2, 10.5_

  - [~] 5.2 Add validation to Deactivate method
    - Add check: throw InvalidOperationException if current status is Expired
    - Set status to Inactive
    - Call UpdateTimestamp()
    - _Requirements: 5.3, 10.6_

  - [~] 5.3 Add validation to Expire method
    - Add check: throw InvalidOperationException if current status is already Expired
    - Set status to Expired
    - Call UpdateTimestamp()
    - _Requirements: 5.1, 10.7_

  - [ ]* 5.4 Write unit tests for invalid status transitions
    - Test Activate on Expired prescription throws InvalidOperationException
    - Test Deactivate on Expired prescription throws InvalidOperationException
    - Test Expire on already Expired prescription throws InvalidOperationException
    - _Requirements: 5.1, 5.2, 5.3_

  - [ ]* 5.5 Write property test for valid status transitions
    - **Property 3: Valid Status Transitions Succeed**
    - **Validates: Requirements 5.4, 5.5, 5.6, 5.7**
    - Generate prescriptions in Active and Inactive states
    - Test Active→Inactive transition succeeds
    - Test Inactive→Active transition succeeds
    - Test Active→Expired transition succeeds
    - Test Inactive→Expired transition succeeds
    - _Requirements: 5.4, 5.5, 5.6, 5.7_

- [ ] 6. Implement timestamp update verification
  - [ ]* 6.1 Write property test for timestamp updates
    - **Property 6: Modifications Update Timestamp**
    - **Validates: Requirements 10.1, 10.2, 10.3, 10.4, 10.5, 10.6, 10.7**
    - Generate valid prescriptions
    - For each modification method (AddMedication, RemoveMedication, UpdateMedication, UpdateNotes, Activate, Deactivate, Expire)
    - Verify UpdatedAt timestamp is more recent after the operation
    - _Requirements: 10.1, 10.2, 10.3, 10.4, 10.5, 10.6, 10.7_

- [ ] 7. Remove DosageDetails value object
  - [~] 7.1 Delete DosageDetails.cs file
    - Remove HealLink.Domain/ValueObjects/DosageDetails.cs
    - _Requirements: 1.2_

  - [~] 7.2 Update any remaining references
    - Search codebase for any remaining DosageDetails references
    - Update or remove as needed
    - _Requirements: 1.2_

- [~] 8. Final checkpoint - Ensure all tests pass
  - Run all unit tests and property tests
  - Verify no compilation errors
  - Ensure all tests pass, ask the user if questions arise.

## Notes

- Tasks marked with `*` are optional and can be skipped for faster implementation
- Each task references specific requirements for traceability
- Property tests should be configured to run minimum 100 iterations
- This is a refactoring, so existing functionality must be preserved
- All validation exceptions should include descriptive error messages
- The refactoring maintains backward compatibility at the database level
