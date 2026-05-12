using FluentValidation;
using System.Net;
using System.Text.Json;

namespace SistemaVentasAPI.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
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

        private async Task HandleExceptionAsync(
            HttpContext context,
            Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = exception switch
            {
                ValidationException validationEx => HandleValidationException(validationEx, context),
                UnauthorizedAccessException unauthorizedEx => HandleUnauthorizedException(unauthorizedEx, context),
                KeyNotFoundException notFoundEx => HandleNotFoundException(notFoundEx, context),
                _ => HandleGenericException(exception, context)
            };

            context.Response.StatusCode = response.StatusCode;

            var json = JsonSerializer.Serialize(response.Body, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(json);
        }

        // ================= HANDLERS =================

        private (int StatusCode, object Body) HandleValidationException(
            ValidationException ex,
            HttpContext context)
        {
            var errors = ex.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key.Replace("LoginRequestDTO.", "").ToLower(),
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            return (
                StatusCodes.Status400BadRequest,
                new
                {
                    status = StatusCodes.Status400BadRequest,
                    title = "Validation error",
                    errors
                }
            );
        }

        private (int StatusCode, object Body) HandleUnauthorizedException(
            UnauthorizedAccessException ex,
            HttpContext context)
        {
            return (
                StatusCodes.Status401Unauthorized,
                new
                {
                    status = StatusCodes.Status401Unauthorized,
                    title = "Unauthorized",
                    message = ex.Message
                }
            );
        }

        private (int StatusCode, object Body) HandleNotFoundException(
            KeyNotFoundException ex,
            HttpContext context)
        {
            return (
                StatusCodes.Status404NotFound,
                new
                {
                    status = StatusCodes.Status404NotFound,
                    title = "Not Found",
                    message = ex.Message
                }
            );
        }

        private (int StatusCode, object Body) HandleGenericException(
            Exception ex,
            HttpContext context)
        {
            return (
                StatusCodes.Status500InternalServerError,
                new
                {
                    status = StatusCodes.Status500InternalServerError,
                    title = "Internal Server Error",
                    message = _env.IsDevelopment()
                        ? ex.Message
                        : "Ocurrió un error inesperado"
                }
            );
        }
    }
}
