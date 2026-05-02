using System;
using healLink.Application.Interfaces;
using HealLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Common;
using HealLink.Infrastructure.Config;
using HealLink.Infrastructure.Helpers;
using HealLink.Infrastructure.Persistence;
using HealLink.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using HealLink.Infrastructure.Services;

namespace HealLink.Infrastructure
{
    public static class InfrastructureDIExtensions 
    {
        public static IServiceCollection AddInfraStructer(this IServiceCollection services, IConfiguration configuration) 
        {
            // Register HTTP context accessor for accessing current user from JWT claims
            services.AddHttpContextAccessor();

            // Register current user service for authorization pipeline
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<EmailBodyBuilder>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorPatientConnectionRepository, DoctorPatientConnectionRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserRoleResolver, UserRoleResolver>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IMedicationReminderRepository, MedicationReminderRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IGuardianRepository, GuardianRepository>();

            services.AddScoped<INotificationPersistenceService, NotificationPersistenceService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
