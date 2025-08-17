using HealLink.Api.Services;
using healLink.Application.Common.Interfaces.Service;


namespace HealLink.Presentation;

public static class DependencyInjection
{
    private static IServiceCollection AddPresentationDocumentation(this IServiceCollection services)
    {
        // Controllers
        services.AddControllers();

        // Swagger + OpenAPI metadata
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // Problem details for consistent error responses
        services.AddProblemDetails();

        // Access to HttpContext outside controllers
        services.AddHttpContextAccessor();


        return services;
    }
    private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;
    }

    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        return services
            .AddPresentationDocumentation()
            .AddServices()
            .AddCorsPolicy();
    }





}
