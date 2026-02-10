# Design Document: Prescription Domain Refactor

## Overview

This design refactors the Prescription domain model to eliminate redundancy between MedicationDosage and DosageDetails value objects, add comprehensive business validations, and improve API clarity. The refactoring consolidates all medication properties into a single MedicationDosage value object and adds validation rules that enforce healthcare prescription business logic.

The refactoring maintains backward compatibility at the database level while improving the domain model's expressiveness and safety. All changes are internal to the domain layer and do not affect external APIs or database schema.

## Architecture

### Current Architecture Issues

1. **Redundant Value Objects**: MedicationDosage contains a DosageDetails property, which itself contains MedicationName and ScheduledTimes - duplicating properties that also exist on MedicationDosage
2. **Missing Validations**: No validation prevents empty medication lists, expired dates in the past, or invalid status transitions
3. **Confusing API**: UpdateInstructions method actually updates the entire medication, not just instructions
4. **Weak Constructor**: Accepts unused 'instructions' parameter and doesn't validate business rules

### Target Architecture

```
Prescription (Entity)
├── PatientId: Guid
├── DoctorId: Guid
├── Notes: string
├── Status: PrescriptionStatus (Active, Inactive, Expired)
├── ExpiresAt: DateTime?
└── Medications: List<MedicationDosage>

MedicationDosage (Value Object)
├── MedicationName: string
├── Dosage: string
├── Instructions: string
└── ScheduledTimes: TimeSpan[]
```

DosageDetails is completely removed from the domain model.

## Components and Interfaces

### MedicationDosage Value Object

**Responsibilities:**
- Encapsulate all information about a single medication
- Enforce validation rules for medication data
- Provide value equality semantics

**Properties:**
- `MedicationName` (string): Name of the medication (required, non-empty)
- `Dosage` (string): Dosage amount/strength (e.g., "500mg", "10ml") (required, non-empty)
- `Instructions` (string): How to take the medication (e.g., "Take with food") (required, non-empty)
- `ScheduledTimes` (TimeSpan[]): When to take medication during the day (required, non-empty array)

**Constructor:**
```csharp
public MedicationDosage(
    string medicationName, 
    string dosage, 
    string instructions, 
    TimeSpan[] scheduledTimes)
```

**Validation Rules:**
- All parameters must be non-null
- MedicationName must not be empty or whitespace
- Dosage must not be empty or whitespace
- Instructions must not be empty or whitespace
- ScheduledTimes array must not be null or empty

**Equality:**
- Override `Equals()` to compare all properties
- Override `GetHashCode()` for hash-based collections
- Two medications are equal if all properties match (including ScheduledTimes order and values)

### Prescription Entity

**Responsibilities:**
- Manage prescription lifecycle and status
- Enforce business rules for medication management
- Maintain audit trail through timestamp updates

**Modified Constructor:**
```csharp
public Prescription(
    Guid patientId, 
    Guid doctorId, 
    string notes, 
    List<MedicationDosage> medications, 
    DateTime? expiresAt = null)
```

**Removed:** `instructions` parameter (unused)

**Constructor Validation:**
- PatientId and DoctorId must be valid Guids
- Medications list must contain at least one medication
- If ExpiresAt is provided, it must not be in the past
- Notes defaults to empty string if null

**Methods:**


1. **AddMedication(MedicationDosage medication)**
   - Validates medication is not null
   - Validates medication with same MedicationName doesn't already exist
   - Adds medication to collection
   - Updates timestamp

2. **RemoveMedication(MedicationDosage medication)**
   - Validates medication is not null
   - Validates medication exists in collection
   - Validates removal won't result in empty medication list
   - Removes medication from collection
   - Updates timestamp

3. **UpdateMedication(MedicationDosage medication)** (renamed from UpdateInstructions)
   - Validates medication is not null
   - Validates medication with matching MedicationName exists
   - Replaces existing medication with new instance
   - Updates timestamp

4. **UpdateNotes(string notes)**
   - Updates notes (defaults to empty string if null)
   - Updates timestamp

5. **Activate()**
   - Validates current status is not Expired
   - Sets status to Active
   - Updates timestamp

6. **Deactivate()**
   - Validates current status is not Expired
   - Sets status to Inactive
   - Updates timestamp

7. **Expire()**
   - Validates current status is not already Expired
   - Sets status to Expired
   - Updates timestamp

**Status Transition Rules:**

```
Active ──────────────> Inactive
  │                       │
  │                       │
  └────> Expired <────────┘
         (terminal)
```

- Active → Inactive: Allowed
- Active → Expired: Allowed
- Inactive → Active: Allowed
- Inactive → Expired: Allowed
- Expired → Any: Not allowed (terminal state)

## Data Models

### MedicationDosage Value Object

```csharp
public class MedicationDosage
{
    public string MedicationName { get; private set; }
    public string Dosage { get; private set; }
    public string Instructions { get; private set; }
    public TimeSpan[] ScheduledTimes { get; private set; }
    
    public MedicationDosage(
        string medicationName, 
        string dosage, 
        string instructions, 
        TimeSpan[] scheduledTimes)
    {
        if (string.IsNullOrWhiteSpace(medicationName))
            throw new ArgumentException("Medication name cannot be empty", nameof(medicationName));
        if (string.IsNullOrWhiteSpace(dosage))
            throw new ArgumentException("Dosage cannot be empty", nameof(dosage));
        if (string.IsNullOrWhiteSpace(instructions))
            throw new ArgumentException("Instructions cannot be empty", nameof(instructions));
        if (scheduledTimes == null || scheduledTimes.Length == 0)
            throw new ArgumentException("Scheduled times cannot be null or empty", nameof(scheduledTimes));
            
        MedicationName = medicationName;
        Dosage = dosage;
        Instructions = instructions;
        ScheduledTimes = scheduledTimes;
    }
    
    public override bool Equals(object obj)
    {
        if (obj is not MedicationDosage other) return false;
        if (ScheduledTimes.Length != other.ScheduledTimes.Length) return false;
        
        for (int i = 0; i < ScheduledTimes.Length; i++)
            if (ScheduledTimes[i] != other.ScheduledTimes[i]) return false;
            
        return MedicationName == other.MedicationName 
            && Dosage == other.Dosage 
            && Instructions == other.Instructions;
    }
    
    public override int GetHashCode()
    {
        return HashCode.Combine(MedicationName, Dosage, Instructions, ScheduledTimes.Length);
    }
}
```

### Prescription Entity (Key Changes)

```csharp
public class Prescription : Entity
{
    // ... existing properties ...
    
    private readonly List<MedicationDosage> _medications = new();
    public IReadOnlyCollection<MedicationDosage> Medications => _medications.AsReadOnly();
    
    public Prescription(
        Guid patientId, 
        Guid doctorId, 
        string notes, 
        List<MedicationDosage> medications, 
        DateTime? expiresAt = null)
    {
        if (medications == null || medications.Count == 0)
            throw new InvalidOperationException("Prescription must contain at least one medication");
            
        if (expiresAt.HasValue && expiresAt.Value < DateTime.UtcNow)
            throw new ArgumentException("Expiration date cannot be in the past", nameof(expiresAt));
        
        PatientId = patientId;
        DoctorId = doctorId;
        Notes = notes ?? string.Empty;
        Status = PrescriptionStatus.Active;
        ExpiresAt = expiresAt;
        _medications.AddRange(medications);
    }
    
    public void UpdateMedication(MedicationDosage medication)
    {
        if (medication == null)
            throw new ArgumentNullException(nameof(medication));
            
        var existingMedication = _medications.Find(m => 
            m.MedicationName == medication.MedicationName);
            
        if (existingMedication == null)
            throw new InvalidOperationException(
                $"Medication '{medication.MedicationName}' not found in prescription");
        
        _medications.Remove(existingMedication);
        _medications.Add(medication);
        UpdateTimestamp();
    }
    
    public void AddMedication(MedicationDosage medication)
    {
        if (medication == null)
            throw new ArgumentNullException(nameof(medication));
            
        if (_medications.Any(m => m.MedicationName == medication.MedicationName))
            throw new InvalidOperationException(
                $"Medication '{medication.MedicationName}' already exists in prescription");
        
        _medications.Add(medication);
        UpdateTimestamp();
    }
    
    public void RemoveMedication(MedicationDosage medication)
    {
        if (medication == null)
            throw new ArgumentNullException(nameof(medication));
            
        if (!_medications.Contains(medication))
            throw new InvalidOperationException(
                $"Medication '{medication.MedicationName}' not found in prescription");
        
        if (_medications.Count == 1)
            throw new InvalidOperationException(
                "Cannot remove the last medication from a prescription");
        
        _medications.Remove(medication);
        UpdateTimestamp();
    }
    
    public void Activate()
    {
        if (Status == PrescriptionStatus.Expired)
            throw new InvalidOperationException(
                "Cannot activate an expired prescription");
        
        Status = PrescriptionStatus.Active;
        UpdateTimestamp();
    }
    
    public void Deactivate()
    {
        if (Status == PrescriptionStatus.Expired)
            throw new InvalidOperationException(
                "Cannot deactivate an expired prescription");
        
        Status = PrescriptionStatus.Inactive;
        UpdateTimestamp();
    }
    
    public void Expire()
    {
        if (Status == PrescriptionStatus.Expired)
            throw new InvalidOperationException(
                "Prescription is already expired");
        
        Status = PrescriptionStatus.Expired;
        UpdateTimestamp();
    }
}
```


## Correctness Properties

*A property is a characteristic or behavior that should hold true across all valid executions of a system—essentially, a formal statement about what the system should do. Properties serve as the bridge between human-readable specifications and machine-verifiable correctness guarantees.*

### Property 1: Constructor Validation Rejects Invalid Inputs

*For any* MedicationDosage constructor call where at least one parameter is null, empty, or whitespace (for strings) or null/empty (for arrays), the constructor should throw an appropriate exception (ArgumentException or ArgumentNullException).

**Validates: Requirements 1.3, 1.4, 1.5, 1.6, 1.7**

### Property 2: Last Medication Cannot Be Removed

*For any* Prescription with exactly one medication, calling RemoveMedication should throw InvalidOperationException and leave the prescription unchanged.

**Validates: Requirements 3.2, 3.3**

### Property 3: Valid Status Transitions Succeed

*For any* Prescription in a non-Expired state, transitioning to any valid target state (Active→Inactive, Inactive→Active, Active→Expired, Inactive→Expired) should succeed and update the status accordingly.

**Validates: Requirements 5.4, 5.5, 5.6, 5.7**

### Property 4: Update Medication Replaces Existing

*For any* Prescription containing a medication with a specific MedicationName, calling UpdateMedication with a new MedicationDosage having the same MedicationName should replace the old medication with the new one, maintaining the same medication count.

**Validates: Requirements 6.4**

### Property 5: MedicationDosage Equality Semantics

*For any* two MedicationDosage instances, they should be equal if and only if all their properties (MedicationName, Dosage, Instructions, and ScheduledTimes including order) are identical.

**Validates: Requirements 8.3, 8.4, 8.5, 8.6**

### Property 6: Modifications Update Timestamp

*For any* Prescription, calling any modification method (AddMedication, RemoveMedication, UpdateMedication, UpdateNotes, Activate, Deactivate, Expire) should result in the UpdatedAt timestamp being more recent than before the operation.

**Validates: Requirements 10.1, 10.2, 10.3, 10.4, 10.5, 10.6, 10.7**

## Error Handling

### Validation Exceptions

**MedicationDosage Constructor:**
- `ArgumentException`: Thrown when MedicationName, Dosage, or Instructions is null, empty, or whitespace
- `ArgumentException`: Thrown when ScheduledTimes is null or empty array

**Prescription Constructor:**
- `InvalidOperationException`: Thrown when medications list is null or empty
- `ArgumentException`: Thrown when ExpiresAt is in the past

**Prescription Methods:**
- `ArgumentNullException`: Thrown when null medication is passed to AddMedication, RemoveMedication, or UpdateMedication
- `InvalidOperationException`: Thrown when:
  - Adding a medication that already exists (duplicate MedicationName)
  - Removing a medication that doesn't exist
  - Removing the last medication from a prescription
  - Updating a medication that doesn't exist
  - Attempting state transitions from Expired status
  - Calling Expire on already Expired prescription

### Exception Messages

All exceptions should include descriptive messages that:
- Clearly state what validation failed
- Include the parameter name or medication name when relevant
- Help developers quickly identify the issue

Example messages:
- "Medication name cannot be empty"
- "Prescription must contain at least one medication"
- "Expiration date cannot be in the past"
- "Medication 'Aspirin' already exists in prescription"
- "Cannot remove the last medication from a prescription"
- "Cannot activate an expired prescription"

## Testing Strategy

### Dual Testing Approach

This refactoring requires both unit tests and property-based tests to ensure comprehensive coverage:

**Unit Tests** focus on:
- Specific edge cases (empty strings, null values, empty arrays)
- Specific error conditions (duplicate medications, invalid state transitions)
- Boundary conditions (removing last medication, past expiration dates)
- Integration between Prescription and MedicationDosage

**Property-Based Tests** focus on:
- Universal properties that hold across all valid inputs
- Generating random valid prescriptions and medications
- Verifying invariants hold after operations
- Testing equality semantics across many combinations

### Property-Based Testing Configuration

**Library:** Use a C# property-based testing library such as:
- FsCheck (recommended for C#)
- CsCheck
- Hedgehog

**Configuration:**
- Minimum 100 iterations per property test
- Each test must reference its design document property
- Tag format: `// Feature: prescription-domain-refactor, Property {number}: {property_text}`

**Example Property Test Structure:**

```csharp
[Property]
public Property ModificationsUpdateTimestamp()
{
    // Feature: prescription-domain-refactor, Property 6: Modifications Update Timestamp
    return Prop.ForAll(
        GenerateValidPrescription(),
        prescription => {
            var beforeTimestamp = prescription.UpdatedAt;
            
            // Perform some modification
            var newMedication = GenerateValidMedication();
            prescription.AddMedication(newMedication);
            
            return prescription.UpdatedAt > beforeTimestamp;
        });
}
```

### Test Coverage Requirements

**MedicationDosage:**
- Unit tests for each validation rule (empty/null inputs)
- Property test for equality semantics (Property 5)
- Unit tests for GetHashCode consistency

**Prescription Constructor:**
- Unit test for empty medications list rejection
- Unit test for past expiration date rejection
- Unit test for null notes handling

**Prescription.AddMedication:**
- Unit test for null medication rejection
- Unit test for duplicate medication rejection
- Property test for timestamp update (Property 6)

**Prescription.RemoveMedication:**
- Unit test for null medication rejection
- Unit test for non-existent medication rejection
- Property test for last medication protection (Property 2)
- Property test for timestamp update (Property 6)

**Prescription.UpdateMedication:**
- Unit test for null medication rejection
- Unit test for non-existent medication rejection
- Property test for medication replacement (Property 4)
- Property test for timestamp update (Property 6)

**Prescription Status Methods:**
- Unit tests for invalid transitions from Expired state
- Property test for valid state transitions (Property 3)
- Property test for timestamp updates (Property 6)

### Migration Testing

Since this is a refactoring, we need to ensure:
1. Existing prescription data can be loaded with the new model
2. All existing tests continue to pass (after updating to new API)
3. No breaking changes to database schema or external APIs

**Migration Validation:**
- Load existing prescription data and verify it deserializes correctly
- Verify MedicationDosage can be constructed from existing data
- Ensure backward compatibility for any external consumers
