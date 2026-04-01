
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using healLink.Application;
using healLink.Application.Commands.Auth;
using healLink.Application.Interfaces;
using HealLink.Application.Interfaces;
using HealLink.Infrastructure;
using HealLink.API.Middleware;
using HealLink.API.Hubs;
using HealLink.Infrastructure.Data;
using HealLink.Infrastructure.Services;
using HealLink.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using HealLink.Contracts.Auth.Validators;

namespace HealLink.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<HealLinkDbContext>(options =>
            {
                var useInMemory = builder.Configuration["UseInMemoryDatabase"] == "true";
                if (useInMemory)
                    options.UseInMemoryDatabase("HealLinkTest");
                else
                    options.UseSqlServer(builder.Configuration.GetConnectionString("localConnection"));
            });
            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<HealLinkDbContext>());

            // Add services to the container.  
            builder.Services.AddInfraStructer(builder.Configuration);
            builder.Services.AddApplication();
            
            // Register SignalR notification service (must be in API layer to avoid circular dependency)
            builder.Services.AddScoped<IRealTimeNotificationService, SignalRNotificationService<NotificationHub>>();
            
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();
            builder.WebHost.UseWebRoot("wwwroot");
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true; // Enable detailed errors for debugging
            });
            builder.Services.AddSignalR();

            // Configure EmailSender
            builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
            builder.Services.AddTransient<IEmailSender>(provider =>
            {
                var emailSettings = provider.GetRequiredService<IOptions<MailSettings>>().Value;
                return new EmailSender(
                    emailSettings.Email,
                    emailSettings.AppPassword,
                    emailSettings.Host,
                    emailSettings.SSL,
                    emailSettings.Port,
                    emailSettings.IsBodyHtml
                );
            });
            builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly));

            // Jwt middleware configuration  
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Secret"])),
                    RoleClaimType = "Role"
                };
                
                // Configure JWT authentication for SignalR
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        
                        // If the request is for SignalR hubs, read the token from query string
                        if (!string.IsNullOrEmpty(accessToken) && 
                            (path.StartsWithSegments("/chatHub") || path.StartsWithSegments("/notificationHub")))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealLink API V1");
                c.RoutePrefix = string.Empty;
            });
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
            if (Directory.Exists(uploadsPath))
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(uploadsPath),
                    RequestPath = "/Uploads"
                });
            }
            
            // Global exception handling middleware
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            
            // Map SignalR hubs
            app.MapHub<NotificationHub>("/notificationHub");
            app.MapHub<ChatHub>("/chatHub");
            
            app.Run();
        }
    }
}
