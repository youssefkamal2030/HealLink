using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HealLink.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            var (statusCode, message) = exception switch
            {
                ValidationException ve => (HttpStatusCode.BadRequest, string.Join("; ", ve.Errors.Select(e => e.ErrorMessage))),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized access"),
                ArgumentNullException => (HttpStatusCode.BadRequest, "Invalid request data"),
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found"),
                InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
            };

            var response = new
            {
                success = false,
                message = message,
                details = _environment.IsDevelopment() ? exception.Message : null,
                stackTrace = _environment.IsDevelopment() ? exception.StackTrace : null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
