# HealLink

HealLink is a healthcare application built with .NET Core that connects patients with doctors, manages prescriptions, and handles medical records.

## Project Structure

The application follows a clean architecture pattern with the following layers:

- **HealLink.API**: The presentation layer containing controllers and API endpoints.
- **HealLink.Contracts**: Contains DTOs, request/response models, and validators.
- **HealLink.Domain**: Contains domain entities, aggregates, and domain events.
- **HealLink.Application**: Contains application logic, commands, queries, and handlers.
- **HealLink.Infrastructure**: Contains implementations of repositories, database context, and external services.

## Deployment with Docker

### Prerequisites

- Docker and Docker Compose installed on your machine

### Local Development

1. Clone the repository
2. Navigate to the project directory
3. Run the application using Docker Compose:

```bash
docker-compose up --build
```

The API will be available at http://localhost:8080

## Deployment on Railway

### Prerequisites

- Railway account (https://railway.app/)
- Railway CLI installed (optional)

### Steps to Deploy

1. Push your code to a GitHub repository

2. Log in to Railway and create a new project

3. Add a new service from GitHub and select your repository

4. Railway will automatically detect the Dockerfile and build your application

5. Add a PostgreSQL or MySQL database service to your project

6. Set up the following environment variables in Railway:
   - `ConnectionStrings__localConnection`: Your database connection string (Railway will provide this)
   - `JwtSettings__Secret`: A strong secret key for JWT token generation
   - `EmailSettings__*`: Configure your email settings
   - `MailSettings__*`: Configure your mail settings

7. Deploy your application

8. Your application will be available at the URL provided by Railway

## Database Migrations

The application uses Entity Framework Core for database migrations. When deploying to a new environment, migrations will be applied automatically on startup.

## Features

- User authentication and authorization
- Patient and doctor profiles
- Prescription management
- Medical history tracking
- Appointment scheduling
- Notifications
- Chat functionality between patients and doctors