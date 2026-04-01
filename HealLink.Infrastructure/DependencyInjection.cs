using System;
using healLink.Application.Interfaces;
using healLink.Application.Repositories;
using HealLink.Domain.Common;
using HealLink.Infrastructure.Config;
using HealLink.Infrastructure.Helpers;
using HealLink.Infrastructure.Persistence;
using HealLink.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using healLink.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using HealLink.Infrastructure.Services;
using HealLink.Application.Interfaces;

namespace HealLink.Infrastructure
{
    public static class InfrastructureDIExtensions 
    {
        public static IServiceCollection AddInfraStructer(this IServiceCollection services, IConfiguration configuration) 
        {
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
            services.AddScoped<IDoctorPatientDoctorPatientConnectionRepository, DoctorPatientConnectionRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IUserRoleResolver, UserRoleResolver>();

            services.AddScoped<INotificationPersistenceService, NotificationPersistenceService>();
            services.AddScoped<INotificationService, NotificationService>();

            return services;
        }
    }
}
