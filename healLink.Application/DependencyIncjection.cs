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
            // Register all FluentValidation validators from the Contracts assembly
            services.AddValidatorsFromAssemblyContaining<HealLink.Contracts.Auth.Validators.RegisterRequestValidator>();

            // Register the validation pipeline behavior — runs before every command/query handler
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
