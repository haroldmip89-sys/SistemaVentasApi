using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Dashboard.DTOs;
using Ventas.Aplicacion.Modelos;
using Ventas.Infraestructura.Context;
using Ventas.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ventas.Infraestructura.Repositorios
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardKpiDTO> GetKpisAsync(int dias)
        {
            var fechaInicio = DateTime.UtcNow.Date.AddDays(-dias);
            var hoy = DateTime.UtcNow.Date;

            var ventas = _context.Ventas.AsQueryable();
            // 1. Calculamos los valores base de forma asíncrona
            var totalVentas = await ventas
                .Where(v => v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta >= fechaInicio)
                .SumAsync(v => (decimal?)v.Total) ?? 0;

            // 2. Calculamos la ganancia total (Numerador)
            var gananciaTotal = await (
                from dv in _context.DetallesVenta
                join p in _context.Productos on dv.IdProducto equals p.IdProducto
                join v in _context.Ventas on dv.IdVenta equals v.IdVenta
                where v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta >= fechaInicio
                select (decimal?)(dv.Cantidad * (dv.PrecioUnitario - p.CostoPromedio))
            ).SumAsync() ?? 0;

            // 3. Calculamos el ingreso total desde los detalles (Denominador)
            var ingresosDetalle = await (
                from dv in _context.DetallesVenta
                join v in _context.Ventas on dv.IdVenta equals v.IdVenta
                where v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta >= fechaInicio
                select (decimal?)(dv.Cantidad * dv.PrecioUnitario)
            ).SumAsync() ?? 0;

            return new DashboardKpiDTO
            {
                TotalVentas = await ventas
                    .Where(v => v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta >= fechaInicio)
                    .SumAsync(v => (decimal?)v.Total) ?? 0,

                CantidadVentas = await ventas
                    .CountAsync(v => v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta >= fechaInicio),

                TotalPendiente = await ventas
                    .Where(v => v.EstadoVenta == EstadoVenta.REGISTRADA)
                    .SumAsync(v => (decimal?)v.Total) ?? 0,

                CantidadPendiente = await ventas
                    .CountAsync(v => v.EstadoVenta == EstadoVenta.REGISTRADA),

                VentasHoy = await ventas
                    .Where(v => v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta.Date == hoy)
                    .SumAsync(v => (decimal?)v.Total) ?? 0,

                CantidadVentasHoy = await ventas
                    .CountAsync(v => v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta.Date == hoy),

                TicketPromedio = await ventas
                    .Where(v => v.EstadoVenta == EstadoVenta.PAGADA && v.FechaVenta >= fechaInicio)
                    .AverageAsync(v => (decimal?)v.Total) ?? 0,

                // 4. Validación Crítica: Si el denominador es 0, la rentabilidad es 0
                Rentabilidad = ingresosDetalle == 0
                    ? 0
                    : Math.Round((gananciaTotal / ingresosDetalle) * 100, 2)
            };
        }
        //  1. INGRESOS vs GANANCIA POR DÍA
        public async Task<List<VentasPorDiaDTO>> GetVentasVsGananciaAsync(int dias)
        {
            var fechaInicio = DateTime.UtcNow.Date.AddDays(-dias);

            var ingresos = await _context.Ventas
                .Where(v => v.EstadoVenta == EstadoVenta.PAGADA &&
                            v.FechaVenta >= fechaInicio)
                .GroupBy(v => v.FechaVenta.Date)
                .Select(g => new
                {
                    Dia = g.Key,
                    TotalVentas = g.Sum(x => x.Total)
                })
                .ToListAsync();

            var ganancias = await (
                from v in _context.Ventas
                join dv in _context.DetallesVenta on v.IdVenta equals dv.IdVenta
                join p in _context.Productos on dv.IdProducto equals p.IdProducto
                where v.EstadoVenta == EstadoVenta.PAGADA &&
                      v.FechaVenta >= fechaInicio
                group new { dv, p } by v.FechaVenta.Date into g
                select new
                {
                    Dia = g.Key,
                    Ganancia = g.Sum(x => x.dv.Cantidad * (x.dv.PrecioUnitario - x.p.CostoPromedio))
                }
            ).ToListAsync();

            var result = ingresos
                .Join(ganancias,
                    i => i.Dia,
                    g => g.Dia,
                    (i, g) => new VentasPorDiaDTO
                    {
                        Dia = i.Dia,
                        TotalVentas = i.TotalVentas,
                        Ganancia = g.Ganancia,
                        Margen = i.TotalVentas == 0
                            ? 0
                            : Math.Round((g.Ganancia * 100) / i.TotalVentas, 2)
                    })
                .OrderByDescending(x => x.Dia)
                .Take(8)
                .ToList();

            return result;
        }

        //  2. TOP PRODUCTOS (MULTIUSO)
        public async Task<List<TopProductoDTO>> GetTopProductosAsync(int dias)
        {
            var fechaInicio = DateTime.UtcNow.Date.AddDays(-dias);

            var data = await (
                from dv in _context.DetallesVenta
                join p in _context.Productos on dv.IdProducto equals p.IdProducto
                join v in _context.Ventas on dv.IdVenta equals v.IdVenta
                where v.EstadoVenta == EstadoVenta.PAGADA &&
                      v.FechaVenta >= fechaInicio
                group new { dv, p } by new { p.IdProducto, p.Nombre } into g
                select new TopProductoDTO
                {
                    Nombre = g.Key.Nombre,
                    TotalVendido = g.Sum(x => x.dv.Cantidad),
                    Ingresos = g.Sum(x => x.dv.Cantidad * x.dv.PrecioUnitario),
                    Ganancia = g.Sum(x => x.dv.Cantidad * (x.dv.PrecioUnitario - x.p.CostoPromedio)),
                    Rentabilidad = g.Sum(x => x.dv.Cantidad * x.dv.PrecioUnitario) == 0
                        ? 0
                        : Math.Round(
                            g.Sum(x => x.dv.Cantidad * (x.dv.PrecioUnitario - x.p.CostoPromedio)) * 100 /
                            g.Sum(x => x.dv.Cantidad * x.dv.PrecioUnitario), 2)
                }
            )
            .OrderByDescending(x => x.Ganancia)
            .Take(5)
            .ToListAsync();

            return data;
        }

        //  3. TOP PRODUCTOS POR FECHA
        public async Task<List<TopProductoDTO>> GetTopProductosPorFechaAsync(DateTime fecha)
        {
            var data = await (
                from dv in _context.DetallesVenta
                join p in _context.Productos on dv.IdProducto equals p.IdProducto
                join v in _context.Ventas on dv.IdVenta equals v.IdVenta
                where v.EstadoVenta == EstadoVenta.PAGADA &&
                      v.FechaVenta.Date == fecha.Date
                group new { dv, p } by new { p.IdProducto, p.Nombre } into g
                select new TopProductoDTO
                {
                    Nombre = g.Key.Nombre,
                    Ganancia = g.Sum(x => x.dv.Cantidad * (x.dv.PrecioUnitario - x.p.CostoPromedio))
                }
            )
            .OrderByDescending(x => x.Ganancia)
            .Take(5)
            .ToListAsync();

            return data;
        }

        //  4. VENTAS POR CATEGORÍA
        public async Task<List<CategoriaVentaDTO>> GetVentasPorCategoriaAsync()
        {
            var data = await (
                from dv in _context.DetallesVenta
                join p in _context.Productos on dv.IdProducto equals p.IdProducto
                join pc in _context.ProductoCategorias on p.IdProducto equals pc.IdProducto
                join c in _context.Categorias on pc.IdCategoria equals c.IdCategoria
                join v in _context.Ventas on dv.IdVenta equals v.IdVenta
                where v.EstadoVenta == EstadoVenta.PAGADA
                group dv by c.Nombre into g
                select new CategoriaVentaDTO
                {
                    Categoria = g.Key,
                    TotalVentas = g.Sum(x => x.Cantidad * x.PrecioUnitario)
                }
            )
            .OrderByDescending(x => x.TotalVentas)
            .ToListAsync();

            return data;
        }

        //  5. FECHAS DISPONIBLES
        public async Task<List<FechaDTO>> GetFechasAsync()
        {
            return await _context.Ventas
                .Where(v => v.EstadoVenta == EstadoVenta.PAGADA)
                .GroupBy(v => v.FechaVenta.Date)
                .Select(g => new FechaDTO
                {
                    Fecha = g.Key
                })
                .OrderByDescending(x => x.Fecha)
                .Take(15)
                .ToListAsync();
        }
    }
}
