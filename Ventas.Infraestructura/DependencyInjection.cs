using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ventas.Aplicacion.Common.Interfaces;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Services;
using Ventas.Infraestructura.Context;
using Ventas.Infraestructura.Repositorios;
using Ventas.Infraestructura.Services;
using Ventas.Infraestructura.StoredProcedures;

namespace Ventas.Infraestructura
{
    public static class DependencyInjection
    {
        public static void AddInfraestructura(
            this IServiceCollection services, IConfiguration configInfo)
        {
            var connectionString = configInfo.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                //options.UseSqlServer(connectionString)
                options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention()
            );
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IMarcaRepository, MarcaRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IImageStorageService, ImageStorageService>();
            services.AddScoped<IStoredProcedureExecutor, StoredProcedureExecutor>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();
            services.AddScoped<IEmailService, EmailService>();
            //services.AddLogger(configInfo);
        }
    }
}
