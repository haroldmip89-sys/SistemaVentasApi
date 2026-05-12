using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Ventas.Infraestructura.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace SistemaVentasAPI.Common
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(TokenService.Secret);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RoleClaimType = ClaimTypes.Role
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var response = new
                            {
                                status = 401,
                                title = "Unauthorized",
                                message = "No estás autenticado o el token es inválido"
                            };

                            await context.Response.WriteAsync(
                                JsonSerializer.Serialize(response));
                        },

                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";

                            var response = new
                            {
                                status = 403,
                                title = "Forbidden",
                                message = "No tienes permisos para acceder a este recurso"
                            };

                            await context.Response.WriteAsync(
                                JsonSerializer.Serialize(response));
                        }
                    };
                });

            return services;
        }
    }
}
