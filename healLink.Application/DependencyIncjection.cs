using FluentValidation;
using healLink.Application.Behaviors;
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

            // Register the validation pipeline behavior — runs before every command/query handler
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
