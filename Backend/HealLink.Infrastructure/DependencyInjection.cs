using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using healLink.Application.Repositories;
using HealLink.Domain.Common;
using HealLink.Infrastructure.Config;
using HealLink.Infrastructure.Helpers;
using HealLink.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using healLink.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using HealLink.Infrastructure.Services;

namespace HealLink.Infrastructure
{
    public static class InfrastructureDIExtensions 
    {
        public static IServiceCollection AddInfraStructer(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IPhotoService, PhotoService>();
            return services;
        }
    }
}
