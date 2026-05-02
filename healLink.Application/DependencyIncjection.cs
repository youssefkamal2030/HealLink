using FluentValidation;
using healLink.Application.Authorization.Policies;
using healLink.Application.Behaviors;
using healLink.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace healLink.Application
{
    public static class ApplicationDIExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register validators from the Contracts assembly (request-level validation)
            services.AddValidatorsFromAssemblyContaining<HealLink.Contracts.Auth.Validators.RegisterRequestValidator>();

            // Register validators from the Application assembly (command-level validation)
            services.AddValidatorsFromAssemblyContaining<healLink.Application.Commands.Auth.LoginCommandValidator>();

            // Register MediatR pipeline behaviors — order matters!
            // Pipeline order: ValidationBehavior → AuthorizationBehavior → Handler
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            // Register authorization policies
            services.AddScoped<IAuthorizationPolicy, ResourceOwnerPolicy>();
            services.AddScoped<IAuthorizationPolicy, PatientOrGuardianAccessPolicy>();

            return services;
        }
    }
}
