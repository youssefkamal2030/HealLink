# HealLink API Documentation

**Base URL (Production):** `https://heallink-production.up.railway.app`  
**Base URL (Local):** `https://localhost:7001`

## 📋 Table of Contents

- [Authentication](#authentication-endpoints)
- [Profile Management](#profile-endpoints)
- [Connection Management](#connection-endpoints)
- [Notifications](#notification-endpoints)
- [Doctor Operations](#doctor-endpoints)
- [Data Models](#data-models)
- [Error Handling](#error-handling)

---

## Authentication Endpoints

### 1. Register User

**Endpoint:** `POST /Auth/register`

**Description:** Registers a new user (Patient or Doctor) in the system.

**Request Body (multipart/form-data):**
```json
{
  "username": "string (min 3 chars)",
  "Password": "string (must contain upper, lower, digit, special char)",
  "Email": "string (valid email)",
  "Role": "Patient|Doctor",
  "PracticeLicenseNumber": "string (required for doctors)",
  "Specialization": "string (required for doctors)",
  "SyndicateId": "file (required for doctors)"
}
```

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

### 2. Login

**Endpoint:** `POST /Auth/login`

**Description:** Authenticates a user and returns a JWT token.

**Request Body:**
```json
{
  "Email": "string",
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

### 3. Forgot Password

**Endpoint:** `POST /Auth/forgot-password`

**Description:** Initiates password reset process by sending a reset link to the user's email.

**Request Body:**
```json
{
  "Email": "string"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "If an account with that email exists, a password reset link has been sent."
  }
  ```

### 4. Reset Password

**Endpoint:** `POST /Auth/reset-password`

**Description:** Resets the user's password using a valid reset token.

**Request Body:**
```json
{
  "Email": "string",
  "Token": "string",
  "NewPassword": "string"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "Password reset Successfully"
  }
  ```

---

## Profile Endpoints

### 1. Get User Profile

**Endpoint:** `GET /api/Profile/{userId}`

**Description:** Retrieves a user's profile information based on their role.

**Response:**
- **Success (200 OK) - Patient:**
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

### 2. Get All Profiles

**Endpoint:** `GET /api/Profile`

**Description:** Retrieves paginated lists of all doctors and patients.

**Query Parameters:**
- `page`: integer (default: 1)
- `pageSize`: integer (default: 20)
- `searchTerm`: string (optional)
- `roleFilter`: string (optional)

### 3. Update Doctor Profile

**Endpoint:** `PUT /api/Profile/doctor/{doctorId}`

**Description:** Updates a doctor's profile information.

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

### 4. Delete Doctor Profile

**Endpoint:** `DELETE /api/Profile/doctor/{doctorId}`

**Description:** Deletes a doctor's profile.

---

## Connection Endpoints

### 1. Request Connection

**Endpoint:** `POST /api/Connections/Request`

**Description:** Patient sends a connection request to a doctor.

**Request Body:**
```json
{
  "doctorId": "guid",
  "patientId": "guid"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "id": "guid",
    "doctorId": "guid",
    "patientId": "guid",
    "status": "Pending",
    "createdAt": "datetime"
  }
  ```

### 2. Accept Connection

**Endpoint:** `POST /api/Connections/Accept`

**Description:** Doctor accepts a patient's connection request.

**Request Body:**
```json
{
  "connectionId": "guid",
  "doctorId": "guid"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "Connection accepted successfully"
  }
  ```

### 3. Reject Connection

**Endpoint:** `POST /api/Connections/Reject`

**Description:** Doctor rejects a patient's connection request.

**Request Body:**
```json
{
  "connectionId": "guid",
  "doctorId": "guid"
}
```

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "Connection rejected successfully"
  }
  ```

### 4. Get Pending Connections for Doctor

**Endpoint:** `GET /api/Connections/Doctor/{doctorId}/Pending`

**Description:** Retrieves all pending connection requests for a doctor.

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Pending connections retrieved successfully.",
    "connections": [
      {
        "id": "guid",
        "doctorId": "guid",
        "patientId": "guid",
        "status": "Pending",
        "createdAt": "datetime",
        "acceptedAt": null
      }
    ],
    "totalCount": 5
  }
  ```

### 5. Get All Connections for Doctor

**Endpoint:** `GET /api/Connections/Doctor/{doctorId}`

**Description:** Retrieves all connections (Pending, Accepted, Rejected) for a doctor.

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Doctor connections retrieved successfully.",
    "connections": [...],
    "totalCount": 10
  }
  ```

### 6. Get All Connections for Patient

**Endpoint:** `GET /api/Connections/Patient/{patientId}`

**Description:** Retrieves all connections for a patient.

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Patient connections retrieved successfully.",
    "connections": [...],
    "totalCount": 3
  }
  ```

---

## Notification Endpoints

### 1. Get Doctor Notifications

**Endpoint:** `GET /api/Notifications/Doctor/{doctorId}`

**Description:** Retrieves all notifications for a specific doctor.

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Notifications retrieved successfully.",
    "notifications": [
      {
        "id": "guid",
        "title": "New Connection Request",
        "message": "You have a new connection request from Patient John Doe.",
        "type": "ConnectionRequest",
        "isRead": false,
        "readAt": null,
        "createdAt": "datetime"
      }
    ],
    "totalCount": 5
  }
  ```

### 2. Get Patient Notifications

**Endpoint:** `GET /api/Notifications/Patient/{patientId}`

**Description:** Retrieves all notifications for a specific patient.

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Notifications retrieved successfully.",
    "notifications": [
      {
        "id": "guid",
        "title": "Connection Accepted",
        "message": "Your connection request has been accepted by the doctor.",
        "type": "ConnectionAccepted",
        "isRead": false,
        "readAt": null,
        "createdAt": "datetime"
      }
    ],
    "totalCount": 3
  }
  ```

### 3. Mark Notification as Read

**Endpoint:** `PUT /api/Notifications/{notificationId}/MarkAsRead`

**Description:** Marks a notification as read.

**Response:**
- **Success (200 OK):**
  ```json
  {
    "message": "Notification marked as read successfully"
  }
  ```

---

## Doctor Endpoints

### 1. Get Connected Patients

**Endpoint:** `GET /Doctors/{doctorId}/ConnectedPatients`

**Description:** Retrieves all patients who have an accepted connection with the doctor.

**Response:**
- **Success (200 OK):**
  ```json
  {
    "success": true,
    "message": "Connected patients retrieved successfully.",
    "connectedPatients": [
      {
        "id": "guid",
        "userId": "guid",
        "fullName": "string",
        "email": "string",
        "guardianId": "guid?",
        "guardianName": "string?"
      }
    ],
    "totalCount": 10
  }
  ```

### 2. Accept Connection (Deprecated)

**Endpoint:** `POST /Doctors/Accept`

**Note:** Use `/api/Connections/Accept` instead. This endpoint remains for backward compatibility.

### 3. Reject Connection (Deprecated)

**Endpoint:** `POST /Doctors/Reject`

**Note:** Use `/api/Connections/Reject` instead. This endpoint remains for backward compatibility.

---

## Data Models

### ConnectionResponse
```csharp
public record ConnectionResponse(
    Guid Id,
    Guid DoctorId,
    Guid PatientId,
    string Status,
    DateTime CreatedAt,
    DateTime? AcceptedAt
);
```

### NotificationResponse
```csharp
public record NotificationResponse(
    Guid Id,
    string Title,
    string Message,
    string Type,
    bool IsRead,
    DateTime? ReadAt,
    DateTime CreatedAt
);
```

### DoctorProfileResponse
```csharp
public record DoctorProfileResponse(
    Guid Id,
    Guid UserId,
    string FullName,
    string Email,
    string Specialization,
    string CurrentWorkplace,
    string PracticeLicenseNumber,
    bool IsApproved,
    bool IsAvailableForChat,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
```

### PatientProfileResponse
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

---

## Error Handling

All endpoints follow consistent error handling patterns:

| Status Code | Description |
|-------------|-------------|
| **200 OK** | Successful operation |
| **400 Bad Request** | Validation errors or invalid input |
| **401 Unauthorized** | Authentication required or failed |
| **404 Not Found** | Resource not found |
| **500 Internal Server Error** | Server-side errors |

**Error Response Format:**
```json
{
  "success": false,
  "message": "Descriptive error message"
}
```

---

## Authentication

Most endpoints require JWT authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your_jwt_token>
```

Get a token by calling the `/Auth/login` endpoint.

---

## Notification Types

| Type | Description |
|------|-------------|
| `ConnectionRequest` | Patient sent a connection request |
| `ConnectionAccepted` | Doctor accepted the connection |
| `ConnectionRejected` | Doctor rejected the connection |

---

## Connection Status

| Status | Description |
|--------|-------------|
| `Pending` | Connection request sent, awaiting doctor action |
| `Accepted` | Doctor accepted the connection |
| `Rejected` | Doctor rejected the connection |

---

## Real-time Features (SignalR)

HealLink uses SignalR for real-time notifications. Connect to the SignalR hub at:

```
wss://heallink-production.up.railway.app/notificationHub
```

**Hub Methods:**
- `ReceiveNotification(NotificationMessage message)` - Receive real-time notifications

---

## Rate Limiting

API endpoints may be rate-limited to prevent abuse. Default limits:
- **100 requests per minute** per IP address
- Check `X-RateLimit-*` headers for current limits

---

## Pagination

List endpoints support pagination through query parameters:
- `page`: Page number (default: 1)
- `pageSize`: Items per page (default: 20, max: 100)

---

## API Versioning

Current API version: **v1.0**

Future versions will be accessible via:
```
/api/v2/endpoint
```

---

## Complete Endpoint Summary

| Category | Endpoint | Method | Auth Required |
|----------|----------|--------|---------------|
| **Auth** | `/Auth/register` | POST | No |
| **Auth** | `/Auth/login` | POST | No |
| **Auth** | `/Auth/forgot-password` | POST | No |
| **Auth** | `/Auth/reset-password` | POST | No |
| **Profiles** | `/api/Profile/{userId}` | GET | Yes |
| **Profiles** | `/api/Profile` | GET | Yes |
| **Profiles** | `/api/Profile/doctor/{doctorId}` | PUT | Yes |
| **Profiles** | `/api/Profile/doctor/{doctorId}` | DELETE | Yes |
| **Connections** | `/api/Connections/Request` | POST | Yes |
| **Connections** | `/api/Connections/Accept` | POST | Yes |
| **Connections** | `/api/Connections/Reject` | POST | Yes |
| **Connections** | `/api/Connections/Doctor/{doctorId}/Pending` | GET | Yes |
| **Connections** | `/api/Connections/Doctor/{doctorId}` | GET | Yes |
| **Connections** | `/api/Connections/Patient/{patientId}` | GET | Yes |
| **Notifications** | `/api/Notifications/Doctor/{doctorId}` | GET | Yes |
| **Notifications** | `/api/Notifications/Patient/{patientId}` | GET | Yes |
| **Notifications** | `/api/Notifications/{notificationId}/MarkAsRead` | PUT | Yes |
| **Doctors** | `/Doctors/{doctorId}/ConnectedPatients` | GET | Yes |
| **Doctors** | `/Doctors/Accept` | POST | Yes |
| **Doctors** | `/Doctors/Reject` | POST | Yes |

**Total Endpoints:** 21

---

## Swagger Documentation

Interactive API documentation is available at:
- **Local:** `https://localhost:7001/swagger`
- **Production:** `https://heallink-production.up.railway.app/swagger`

---

## Support

For API support or questions:
- **Email:** support@heallink.com
- **Documentation:** [GitHub Repository](https://github.com/yourusername/heallink)

---

**Last Updated:** January 3, 2026  
**API Version:** 1.0