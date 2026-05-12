using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ventas.Aplicacion.Common.Behaviours;
using FluentValidation;

namespace Ventas.Aplicacion
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplicacion(
            this IServiceCollection services
            )
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            return services;
        }
        
    }
}
