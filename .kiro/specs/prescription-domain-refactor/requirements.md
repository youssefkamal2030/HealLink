# Requirements Document

## Introduction

This document specifies the requirements for refactoring the Prescription domain model in the HealLink healthcare application. The refactoring addresses structural redundancy between MedicationDosage and DosageDetails value objects, adds missing business validations, and improves method naming clarity. The goal is to create a clean, maintainable domain model that properly enforces healthcare prescription business rules.

## Glossary

- **Prescription**: A medical prescription entity containing patient information, doctor information, medications, and validity period
- **MedicationDosage**: A value object representing a single medication with its dosage, instructions, and scheduled times
- **DosageDetails**: A redundant value object to be removed during refactoring
- **PrescriptionStatus**: An enumeration with values Active, Inactive, and Expired
- **ScheduledTimes**: An array of TimeSpan values indicating when a medication should be taken during the day
- **Domain_Model**: The collection of entities and value objects representing the prescription domain

## Requirements

### Requirement 1: Consolidate Value Objects

**User Story:** As a developer, I want a single consolidated MedicationDosage value object, so that I eliminate redundancy and simplify the domain model.

#### Acceptance Criteria

1. THE MedicationDosage SHALL contain MedicationName, Dosage, Instructions, and ScheduledTimes properties
2. THE Domain_Model SHALL NOT contain the DosageDetails value object
3. WHEN MedicationDosage is constructed, THE MedicationDosage SHALL validate that all required properties are non-null
4. WHEN MedicationDosage is constructed with empty or whitespace MedicationName, THE MedicationDosage SHALL throw ArgumentException
5. WHEN MedicationDosage is constructed with empty or whitespace Dosage, THE MedicationDosage SHALL throw ArgumentException
6. WHEN MedicationDosage is constructed with empty or whitespace Instructions, THE MedicationDosage SHALL throw ArgumentException
7. WHEN MedicationDosage is constructed with null or empty ScheduledTimes array, THE MedicationDosage SHALL throw ArgumentException

### Requirement 2: Remove Unused Constructor Parameters

**User Story:** As a developer, I want the Prescription constructor to only accept necessary parameters, so that the API is clear and prevents confusion.

#### Acceptance Criteria

1. THE Prescription constructor SHALL NOT have an instructions parameter
2. THE Prescription constructor SHALL accept patientId, doctorId, notes, medications, and expiresAt parameters
3. WHEN Prescription is constructed with null medications list, THE Prescription SHALL initialize with an empty medications collection
4. WHEN Prescription is constructed with null notes, THE Prescription SHALL initialize notes as empty string

### Requirement 3: Validate Non-Empty Medication Lists

**User Story:** As a healthcare system, I want prescriptions to always contain at least one medication, so that invalid prescriptions cannot be created or persisted.

#### Acceptance Criteria

1. WHEN Prescription is constructed with an empty medications list, THE Prescription SHALL throw InvalidOperationException
2. WHEN the last medication is removed from a Prescription, THE Prescription SHALL throw InvalidOperationException
3. WHEN RemoveMedication is called and would result in zero medications, THE Prescription SHALL prevent the removal and throw InvalidOperationException

### Requirement 4: Validate Expiration Dates

**User Story:** As a healthcare system, I want to enforce valid expiration dates, so that prescriptions cannot expire in the past or have illogical validity periods.

#### Acceptance Criteria

1. WHEN Prescription is constructed with an ExpiresAt date in the past, THE Prescription SHALL throw ArgumentException
2. WHEN a Prescription's ExpiresAt date is reached or passed, THE Prescription SHALL be considered expired
3. IF ExpiresAt is null, THEN THE Prescription SHALL be considered to have no expiration date

### Requirement 5: Enforce Status Transition Rules

**User Story:** As a healthcare system, I want to enforce valid prescription status transitions, so that prescriptions follow proper lifecycle management.

#### Acceptance Criteria

1. WHEN Expire is called on a Prescription with status Expired, THE Prescription SHALL throw InvalidOperationException
2. WHEN Activate is called on a Prescription with status Expired, THE Prescription SHALL throw InvalidOperationException
3. WHEN Deactivate is called on a Prescription with status Expired, THE Prescription SHALL throw InvalidOperationException
4. THE Prescription SHALL allow transition from Active to Inactive
5. THE Prescription SHALL allow transition from Inactive to Active
6. THE Prescription SHALL allow transition from Active to Expired
7. THE Prescription SHALL allow transition from Inactive to Expired

### Requirement 6: Improve Method Naming Clarity

**User Story:** As a developer, I want method names to accurately reflect their behavior, so that the API is intuitive and prevents misuse.

#### Acceptance Criteria

1. THE Prescription SHALL have a method named UpdateMedication that replaces an existing medication
2. THE Prescription SHALL NOT have a method named UpdateInstructions
3. WHEN UpdateMedication is called with a medication that doesn't exist, THE Prescription SHALL throw InvalidOperationException
4. WHEN UpdateMedication is called with a valid existing medication, THE Prescription SHALL replace the old medication with the new one

### Requirement 7: Validate Medication Operations

**User Story:** As a healthcare system, I want medication operations to be validated, so that only valid modifications can be made to prescriptions.

#### Acceptance Criteria

1. WHEN AddMedication is called with null medication, THE Prescription SHALL throw ArgumentNullException
2. WHEN AddMedication is called with a medication that already exists (same MedicationName), THE Prescription SHALL throw InvalidOperationException
3. WHEN RemoveMedication is called with null medication, THE Prescription SHALL throw ArgumentNullException
4. WHEN RemoveMedication is called with a medication that doesn't exist, THE Prescription SHALL throw InvalidOperationException

### Requirement 8: Implement Value Object Equality

**User Story:** As a developer, I want MedicationDosage to support proper equality comparison, so that medications can be correctly identified and compared.

#### Acceptance Criteria

1. THE MedicationDosage SHALL override Equals method
2. THE MedicationDosage SHALL override GetHashCode method
3. WHEN two MedicationDosage instances have identical properties, THE MedicationDosage SHALL return true for Equals
4. WHEN two MedicationDosage instances have different MedicationName, THE MedicationDosage SHALL return false for Equals
5. WHEN two MedicationDosage instances have different ScheduledTimes arrays, THE MedicationDosage SHALL return false for Equals
6. WHEN comparing ScheduledTimes arrays, THE MedicationDosage SHALL consider order and values

### Requirement 9: Maintain Immutability of Value Objects

**User Story:** As a developer, I want MedicationDosage to be immutable, so that it follows value object patterns and prevents unintended modifications.

#### Acceptance Criteria

1. THE MedicationDosage SHALL have only private setters for all properties
2. THE MedicationDosage SHALL NOT provide methods that modify its state
3. WHEN MedicationDosage needs to be changed, THE Prescription SHALL create a new MedicationDosage instance

### Requirement 10: Update Timestamp on Modifications

**User Story:** As a healthcare system, I want prescriptions to track when they were last modified, so that audit trails are maintained.

#### Acceptance Criteria

1. WHEN AddMedication is called successfully, THE Prescription SHALL update its timestamp
2. WHEN RemoveMedication is called successfully, THE Prescription SHALL update its timestamp
3. WHEN UpdateMedication is called successfully, THE Prescription SHALL update its timestamp
4. WHEN UpdateNotes is called, THE Prescription SHALL update its timestamp
5. WHEN Activate is called successfully, THE Prescription SHALL update its timestamp
6. WHEN Deactivate is called successfully, THE Prescription SHALL update its timestamp
7. WHEN Expire is called successfully, THE Prescription SHALL update its timestamp
