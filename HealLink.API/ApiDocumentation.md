# HealLink API Documentation

**Base URL:** `https://heallink-production.up.railway.app`

## Overview

The HealLink API provides endpoints for healthcare management, including user authentication, profile management, and healthcare provider/patient interactions.

## Authentication Endpoints

### Register User

**Endpoint:** `POST /Auth/register`

**Description:** Registers a new user (Patient, Doctor, or Admin) in the system.

**Request Body (multipart/form-data):**
```json
{
  "username": "string (min 3 chars)",
  "Password": "string (must contain upper, lower, digit, special char)",
  "Email": "string (valid email)",
  "Role": "Patient|Doctor|Admin",
  "Idfront": "file (optional)",
  "Idback": "file (optional)"
}
```

**Validation Rules:**
- `username`: Required, minimum 3 characters
- `Password`: Required, must contain at least one uppercase letter, one lowercase letter, one digit, and one special character
- `Email`: Required, must be a valid email address
- `Role`: Must be one of `Patient`, `Doctor`, or `Admin`

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "User registered successfully"
  }
  ```
- **Failure (400 Bad Request):**
  ```json
  {
    "message": "<error message>"
  }
  ```

### Login

**Endpoint:** `POST /Auth/login`

**Description:** Authenticates a user and returns a JWT token.

**Request Body:**
```json
{
  "Email": "string (valid email)",
  "Password": "string"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "token": "<jwt_token>"
  }
  ```
- **Failure (401 Unauthorized):**
  ```json
  "Invalid credentials"
  ```

### Forgot Password

**Endpoint:** `POST /Auth/forgot-password`

**Description:** Initiates a password reset process by sending a reset link to the user's email.

**Request Body:**
```json
{
  "Email": "string (valid email)"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "If an account with that email exists, a password reset link has been sent."
  }
  ```

### Reset Password

**Endpoint:** `POST /Auth/reset-password`

**Description:** Resets the user's password using a valid reset token.

**Request Body:**
```json
{
  "Email": "string (valid email)",
  "Token": "string (reset token)",
  "NewPassword": "string (must contain upper, lower, digit, special char)"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "Password reset Successfully"
  }
  ```
- **Failure (400 Bad Request):**
  ```json
  {
    "message": "<error message>"
  }
  ```

## Profile Endpoints

### Get User Profile

**Endpoint:** `GET /api/Profile/{userId}`

**Description:** Retrieves a user's profile information based on their role (Patient or Doctor).

**Path Parameters:**
- `userId`: GUID of the user

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Profile retrieved successfully",
    "patientProfile": {
      "id": "guid",
      "userId": "guid",
      "fullName": "string",
      "email": "string",
      "guardianId": "guid?",
      "guardianName": "string?",
      "createdAt": "datetime",
      "updatedAt": "datetime"
    }
  }
  ```
  OR
  ```json
  {
    "success": true,
    "message": "Profile retrieved successfully",
    "doctorProfile": {
      "id": "guid",
      "userId": "guid",
      "fullName": "string",
      "email": "string",
      "phone": "string",
      "specialization": "string",
      "currentWorkplace": "string",
      "licenseNumber": "string",
      "practiceLicenseNumber": "string",
      "address": "string",
      "isApproved": true,
      "isAvailableForChat": true,
      "createdAt": "datetime",
      "updatedAt": "datetime"
    }
  }
  ```
- **Failure (404 Not Found):**
  ```json
  {
    "success": false,
    "message": "Profile not found for the specified user"
  }
  ```

### Get All Profiles

**Endpoint:** `GET /api/Profile`

**Description:** Retrieves paginated lists of all doctors and patients with optional filtering.

**Query Parameters:**
- `page`: integer (default: 1)
- `pageSize`: integer (default: 20)
- `searchTerm`: string (optional)
- `roleFilter`: string (optional)

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Retrieved X doctors and Y patients successfully",
    "doctors": [
      {
        "id": "guid",
        "userId": "guid",
        "fullName": "string",
        "email": "string",
        "phone": "string",
        "specialization": "string",
        "currentWorkplace": "string",
        "licenseNumber": "string",
        "practiceLicenseNumber": "string",
        "address": "string",
        "isApproved": true,
        "isAvailableForChat": true,
        "createdAt": "datetime",
        "updatedAt": "datetime"
      }
    ],
    "patients": [
      {
        "id": "guid",
        "userId": "guid",
        "fullName": "string",
        "email": "string",
        "guardianId": "guid?",
        "guardianName": "string?",
        "createdAt": "datetime",
        "updatedAt": "datetime"
      }
    ],
    "totalCount": 0
  }
  ```

### Update Doctor Profile

**Endpoint:** `PUT /api/Profile/doctor/{doctorId}`

**Description:** Updates a doctor's profile information.

**Path Parameters:**
- `doctorId`: GUID of the doctor

**Request Body:**
```json
{
  "fullName": "string",
  "gender": "string",
  "nationality": "string",
  "street": "string",
  "city": "string",
  "state": "string",
  "country": "string",
  "specialization": "string",
  "currentWorkplace": "string",
  "phone": "string",
  "isAvailableForChat": true
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Doctor profile updated successfully"
  }
  ```
- **Failure (400 Bad Request):**
  ```json
  {
    "success": false,
    "message": "<error message>"
  }
  ```

### Delete Doctor Profile

**Endpoint:** `DELETE /api/Profile/doctor/{doctorId}`

**Description:** Deletes a doctor's profile.

**Path Parameters:**
- `doctorId`: GUID of the doctor

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Doctor profile deleted successfully"
  }
  ```
- **Failure (404 Not Found):**
  ```json
  {
    "success": false,
    "message": "Doctor profile not found"
  }
  ```

## Data Models

### Doctor Profile Response
```csharp
public record DoctorProfileResponse(
    Guid Id,
    Guid UserId,
    string FullName,
    string Email,
    string Phone,
    string Specialization,
    string CurrentWorkplace,
    string LicenseNumber,
    string PracticeLicenseNumber,
    string Address,
    bool IsApproved,
    bool IsAvailableForChat,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
```

### Patient Profile Response
```csharp
public record PatientProfileResponse(
    Guid Id,
    Guid UserId,
    string FullName,
    string Email,
    Guid? GuardianId,
    string? GuardianName,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
```

## Error Handling

All endpoints follow consistent error handling patterns:

- **400 Bad Request**: Validation errors or invalid input
- **401 Unauthorized**: Authentication failures
- **404 Not Found**: Resource not found
- **500 Internal Server Error**: Server-side errors

Error responses follow this format:
```json
{
  "success": false,
  "message": "Descriptive error message"
}
```

## Rate Limiting

API endpoints may be rate-limited to prevent abuse. Check response headers for rate limit information.

## Authentication

Most endpoints require JWT authentication. Include the token in the Authorization header:
```
Authorization: Bearer <your_jwt_token>
```

## Content Types

- **Request**: `application/json` (except file uploads)
- **Response**: `application/json`
- **File Uploads**: `multipart/form-data`

## Pagination

List endpoints support pagination through `page` and `pageSize` query parameters.

## Filtering and Search

The `GetAllProfiles` endpoint supports:
- `searchTerm`: Search across name, email, and specialization fields
- `roleFilter`: Filter by user role ("Doctor" or "Patient")