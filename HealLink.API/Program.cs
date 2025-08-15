
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using healLink.Application.Commands.Auth;
using healLink.Application.Interfaces;
using HealLink.Contracts.Auth;
using HealLink.Infrastructure;
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

namespace HealLink
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<HealLinkDbContext>(options =>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteConnection")));
            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<HealLinkDbContext>());

            // Add services to the container.  
            builder.Services.AddInfraStructer(builder.Configuration); 
            builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>());
            builder.Services.AddSwaggerGen();
            builder.WebHost.UseWebRoot("wwwroot");

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
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.  
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealLink API V1");
                    c.RoutePrefix = "swagger";
                });
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads")),
                RequestPath = "/Uploads"
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
