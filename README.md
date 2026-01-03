# 🏥 HealLink - Healthcare Management Platform

**HealLink** is a comprehensive healthcare management platform built with .NET 9, designed to connect patients with doctors, manage medical records, facilitate real-time communication, and streamline healthcare service delivery.

## 📋 Table of Contents

- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Project Architecture](#-project-architecture)
- [Getting Started](#-getting-started)
- [API Documentation](#-api-documentation)
- [Database](#-database)
- [Deployment](#-deployment)

---

## ✨ Features

### 👨‍⚕️ Doctor Features
- **Profile Management** - Complete doctor profiles with specialization, license verification, and workplace details
- **Patient Connections** - Accept/reject patient connection requests
- **Connected Patients Dashboard** - View and manage all connected patients
- **Real-time Notifications** - Get notified of new connection requests via SignalR
- **Availability Status** - Control availability for chat consultations

### 👤 Patient Features
- **Profile Management** - Manage personal health information
- **Doctor Discovery** - Browse and connect with verified doctors
- **Connection Requests** - Send connection requests to doctors
- **Real-time Notifications** - Receive updates on connection status
- **Medical Records Access** - View and manage medical history

### 🔔 Notification System
- **Real-time Delivery** - SignalR-powered instant notifications
- **Persistent Storage** - All notifications saved to database for offline access
- **Read/Unread Tracking** - Mark notifications as read with timestamp
- **Type-based Categorization** - Different notification types for different events

### 🔐 Authentication & Authorization
- **JWT-based Authentication** - Secure token-based auth system
- **Role-based Access Control** - Doctor, Patient, and Admin roles
- **Password Reset** - Email-based password recovery
- **Doctor Verification** - License and syndicate ID verification for doctors

### 📊 Data Management
- **Clean Architecture** - CQRS pattern with MediatR
- **Domain Events** - Event-driven architecture for decoupled components
- **Repository Pattern** - Abstracted data access layer
- **Entity Framework Core** - Code-first database approach

---

## 🛠️ Tech Stack

### Backend
- **.NET 9** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API
- **Entity Framework Core 9** - ORM for database access
- **MediatR** - CQRS and mediator pattern implementation
- **SignalR** - Real-time communication

### Database
- **SQL Server** - Primary database
- **Code-First Migrations** - Database schema management

### Architecture Patterns
- **Clean Architecture** - Separation of concerns
- **CQRS** - Command Query Responsibility Segregation
- **Domain-Driven Design** - Rich domain models
- **Repository Pattern** - Data access abstraction
- **Domain Events** - Event-driven architecture

### Libraries & Tools
- **FluentValidation** - Request validation
- **AutoMapper** - Object mapping
- **JWT Authentication** - Secure token-based auth
- **Docker** - Containerization

---

## 🏗️ Project Architecture

The application follows a **Clean Architecture** pattern with clear separation of concerns:

```
HealLink/
├── HealLink.API/                    # 🌐 Presentation Layer
│   ├── Controllers/                 # API endpoints
│   ├── Middleware/                  # Custom middleware
│   └── Program.cs                   # Application entry point
│
├── HealLink.Contracts/              # 📄 DTOs & Contracts
│   ├── Auth/                        # Authentication DTOs
│   ├── Connections/                 # Connection request/response models
│   ├── Notifications/               # Notification DTOs
│   ├── Profile/                     # Profile DTOs
│   └── Validators/                  # FluentValidation rules
│
├── HealLink.Application/            # 💼 Application Logic
│   ├── Commands/                    # Write operations
│   ├── Queries/                     # Read operations
│   ├── Handlers/                    # Command/Query handlers
│   ├── Repositories/                # Repository interfaces
│   └── Common/                      # Shared application logic
│
├── HealLink.Domain/                 # 🎯 Domain Layer
│   ├── Entities/                    # Domain entities & aggregates
│   ├── Enums/                       # Domain enumerations
│   ├── ValueObjects/                # Value objects
│   ├── DomainEvents/                # Domain events
│   └── Base/                        # Base entity classes
│
└── HealLink.Infrastructure/         # 🔧 Infrastructure Layer
    ├── Data/                        # DbContext & configurations
    ├── Repositories/                # Repository implementations
    ├── Services/                    # External services (Email, SignalR)
    └── Migrations/                  # EF Core migrations
```

### Layer Responsibilities

| Layer | Responsibility | Dependencies |
|-------|---------------|-------------|
| **API** | HTTP endpoints, routing, middleware | → Application, Contracts |
| **Contracts** | DTOs, request/response models, validation | None |
| **Application** | Business logic, use cases, CQRS handlers | → Domain, Contracts |
| **Domain** | Entities, business rules, domain events | None (Core) |
| **Infrastructure** | Database, external services, repositories | → Application, Domain |

---

## 🚀 Getting Started

### Prerequisites

- **.NET 9 SDK** - [Download](https://dotnet.microsoft.com/download/dotnet/9.0)
- **SQL Server** - Local or remote instance
- **Docker** (optional) - For containerized deployment

### Local Development

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/heallink.git
   cd heallink
   ```

2. **Update connection string**
   
   Edit `appsettings.Development.json`:
   ```json
   {
     "ConnectionStrings": {
       "localConnection": "Server=localhost;Database=HealLinkDb;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd HealLink.API
   dotnet ef database update --project ../HealLink.Infrastructure
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

   The API will be available at: `https://localhost:7001`

5. **Explore the API**
   - Swagger UI: `https://localhost:7001/swagger`
   - API Documentation: See [ApiDocumentation.md](./HealLink.API/ApiDocumentation.md)

---

## 📚 API Documentation

Comprehensive API documentation is available in [ApiDocumentation.md](./HealLink.API/ApiDocumentation.md).

### Quick Reference

| Endpoint Group | Description | Count |
|----------------|-------------|-------|
| **Authentication** | Register, login, password reset | 4 endpoints |
| **Profiles** | User profile management | 4 endpoints |
| **Connections** | Patient-doctor connections | 6 endpoints |
| **Notifications** | Notification management | 3 endpoints |
| **Doctors** | Doctor-specific operations | 3 endpoints |

**Total API Endpoints:** 20+

---

## 🗄️ Database

### Database Schema Overview

**Main Entities:**
- **Users** - Authentication and user management
- **Doctors** - Doctor-specific information and verification
- **Patients** - Patient profiles and guardians
- **DoctorPatientConnections** - Manages patient-doctor relationships
- **Notifications** - Notification history and tracking
- **Prescriptions** - Medical prescriptions
- **MedicalHistory** - Patient medical records
- **ChatMessages** - Real-time messaging history

### Migrations

**Create a new migration:**
```bash
dotnet ef migrations add MigrationName --project HealLink.Infrastructure --startup-project HealLink.API
```

**Apply migrations:**
```bash
dotnet ef database update --project HealLink.Infrastructure --startup-project HealLink.API
```

**Remove last migration:**
```bash
dotnet ef migrations remove --project HealLink.Infrastructure --startup-project HealLink.API
```

---

## 🚢 Deployment

### Docker Deployment

**Build and run with Docker Compose:**
```bash
docker-compose up --build
```

The application will be available at: `http://localhost:8080`

**Docker Hub:**
```bash
docker pull yourusername/heallink:latest
docker run -p 8080:80 yourusername/heallink:latest
```

### Railway Deployment

1. **Push to GitHub**
   ```bash
   git push origin main
   ```

2. **Deploy to Railway**
   - Log in to [Railway](https://railway.app/)
   - Create new project from GitHub repository
   - Railway auto-detects Dockerfile

3. **Configure Environment Variables**
   ```
   ConnectionStrings__localConnection=<database_connection_string>
   JwtSettings__Secret=<your_jwt_secret_key>
   JwtSettings__Issuer=HealLink
   JwtSettings__Audience=HealLinkUsers
   EmailSettings__SmtpServer=<smtp_server>
   EmailSettings__SmtpPort=587
   EmailSettings__SenderEmail=<sender_email>
   EmailSettings__SenderPassword=<email_password>
   ```

4. **Add Database Service**
   - Add PostgreSQL or SQL Server from Railway marketplace
   - Use provided connection string

5. **Deploy**
   - Railway automatically builds and deploys
   - Access your app at the provided Railway URL

### Production URL

Live API: `https://heallink-production.up.railway.app`

---

## 🔑 Key Design Decisions

### 1. **Notification Schema Redesign**
- Uses `DoctorId`/`PatientId` instead of generic `UserId`
- Simplifies frontend queries (no joins required)
- Dual delivery: Database + SignalR (persistence + real-time)

### 2. **Connection Management**
- Consolidated `ConnectionRequest` → `DoctorPatientConnection`
- Eliminated table redundancy
- Domain events trigger notifications automatically

### 3. **Repository Pattern**
- Clean abstraction over data access
- Facilitates unit testing
- Maintains clean architecture boundaries

### 4. **CQRS with MediatR**
- Separate read/write operations
- Improves scalability
- Clear separation of concerns

---

## 📝 License

This project is licensed under the MIT License.

---

## 👥 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

---

## 📧 Contact

For questions or support, please contact: [your-email@example.com]

---

**Built with ❤️ using .NET 9**