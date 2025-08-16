# HealLink Auth API Documentation

**Base URL:** `https://creative-endlessly-bullfrog.ngrok-free.app`

---

## Register

- **Endpoint:** `POST /Auth/register`
- **Description:** Registers a new user (Patient, Doctor, or Admin).

### Request Body (multipart/form-data)
```json
{
  "username": "string (min 3 chars)",
  "Password": "string (must contain upper, lower, digit, special char)",
  "Email": "string (valid email)",
  "Role": "Patient|Doctor|Admin",
  "PracticeLicenseNumber": "string (required for doctors)",
  "Specialization": "string (required for doctors)",
  "SyndicateId": "file (required for doctors)",
  "Idfront": "file (optional)",
  "Idback": "file (optional)"
}
```

### Validation Rules
- `username`: Required, minimum 3 characters
- `Password`: Required, must contain at least one uppercase letter, one lowercase letter, one digit, and one special character
- `Email`: Required, must be a valid email address
- `Role`: Must be one of `Patient`, `Doctor`, or `Admin`
- `PracticeLicenseNumber`: Required when Role is `Doctor`
- `Specialization`: Required when Role is `Doctor`
- `SyndicateId`: Required file upload when Role is `Doctor`

### Response
- **Success:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "Message": "User registered successfully"
    }
    ```
- **Failure:**
  - **Status:** 400 Bad Request
  - **Body:**
    ```json
    {
      "Message": "<error message>"
    }
    ```

---

## Login

- **Endpoint:** `POST /Auth/login`
- **Description:** Authenticates a user and returns a JWT token.

### Request Body
```json
{
  "Email": "string (valid email)",
  "Password": "string"
}
```

### Validation Rules
- `Email`: Required, must be a valid email address
- `Password`: Required

### Response
- **Success:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "token": "<jwt_token>"
    }
    ```
- **Failure:**
  - **Status:** 401 Unauthorized
  - **Body:**
    ```json
    "Invalid credentials"
    ```

---

## Forgot Password

- **Endpoint:** `POST /Auth/forgot-password`
- **Description:** Initiates a password reset process by sending a reset link to the user's email if it exists.

### Request Body
```json
{
  "Email": "string (valid email)"
}
```

### Validation Rules
- `Email`: Required, must be a valid email address

### Response
- **Success:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "Message": "If an account with that email exists, a password reset link has been sent."
    }
    ```
- **Failure:**
  - **Status:** 400 Bad Request
  - **Body:**
    ```json
    {
      "Message": "<error message>"
    }
    ```

---

## Reset Password

- **Endpoint:** `POST /Auth/reset-password`
- **Description:** Resets the user's password using a valid reset token.

### Request Body
```json
{
  "Email": "string (valid email)",
  "Token": "string (reset token)",
  "NewPassword": "string (must contain upper, lower, digit, special char)"
}
```

### Validation Rules
- `Email`: Required, must be a valid email address
- `Token`: Required, must be a valid reset token
- `NewPassword`: Required, must contain at least one uppercase letter, one lowercase letter, one digit, and one special character

### Response
- **Success:**
  - **Status:** 200 OK
  - **Body:**
    ```json
    {
      "Message": "Password reset Successfully"
    }
    ```
- **Failure:**
  - **Status:** 400 Bad Request
  - **Body:**
    ```json
    {
      "Message": "<error message>"
    }
    ```

---

## Error Handling
- All validation errors will return HTTP 400 with a descriptive message.
- Invalid credentials on login will return HTTP 401 with message `Invalid credentials`.
- Invalid role on registration will return HTTP 400 with message `Invalid role`.

---

## Example Usage

### Register Example
```bash
curl -X POST "https://creative-endlessly-bullfrog.ngrok-free.app/Auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "john_doe",
    "Password": "Password@123",
    "Email": "john@example.com",
    "Role": "Patient"
  }'
```

### Login Example
```bash
curl -X POST "https://creative-endlessly-bullfrog.ngrok-free.app/Auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "Email": "john@example.com",
    "Password": "Password@123"
  }'
```

### Forgot Password Example
```bash
curl -X POST "https://creative-endlessly-bullfrog.ngrok-free.app/Auth/forgot-password" \
  -H "Content-Type: application/json" \
  -d '{
    "Email": "john@example.com"
  }'
```

### Reset Password Example
```bash
curl -X POST "https://creative-endlessly-bullfrog.ngrok-free.app/Auth/reset-password" \
  -H "Content-Type: application/json" \
  -d '{
    "Email": "john@example.com",
    "Token": "<reset_token>",
    "NewPassword": "NewPassword@123"
  }'
```