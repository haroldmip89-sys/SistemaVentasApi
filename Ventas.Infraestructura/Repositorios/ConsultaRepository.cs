using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;
using Ventas.Infraestructura.Context;

namespace Ventas.Infraestructura.Repositorios
{
    public class ConsultaRepository : IConsultaRepository
    {
        private readonly ApplicationDbContext _context;

        public ConsultaRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Venta?> GetVentaByIdAsync(int idVenta)
        {
            return await _context.Ventas
                .Include(v => v.Detalles) // Importante para ver qué se vendió
                    .ThenInclude(d => d.Producto)
                .Include(v => v.ComprobanteElectronico)
                .Include(v => v.Usuario) // Para saber quién vendió
                .FirstOrDefaultAsync(v => v.IdVenta == idVenta);
        }

        public async Task<List<Venta>> GetVentasAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, int? idUsuario)
        {
            var query = _context.Ventas
                .Include(v => v.Usuario) // Incluimos el usuario para ver quién vendió
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Producto)
                .Include(v => v.ComprobanteElectronico)
                .OrderByDescending(v => v.FechaVenta)
                .AsQueryable();

            // 1. Filtro por Estado (Enum)
            if (!string.IsNullOrEmpty(estado) && Enum.TryParse<EstadoVenta>(estado, true, out var estadoEnum))
            {
                query = query.Where(v => v.EstadoVenta == estadoEnum);
            }

            // 2. Filtro por Usuario
            if (idUsuario.HasValue && idUsuario > 0)
            {
                query = query.Where(v => v.IdUsuario == idUsuario);
            }

            // 3. Filtro por Rango de Fechas
            if (fechaInicio.HasValue)
            {
                query = query.Where(v => v.FechaVenta >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                // Para incluir todo el día de la fecha fin (23:59:59)
                var finDia = fechaFin.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(v => v.FechaVenta <= finDia);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Compra>> GetComprasAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, int? idUsuario)
        {
            var query = _context.Compras
                .Include(c => c.Proveedor)
                .Include(c => c.Usuario)
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.Producto)
                .OrderByDescending(c => c.FechaCompra)
                .AsQueryable();

            // 1. Filtro por Estado (Enum)
            if (!string.IsNullOrEmpty(estado) && Enum.TryParse<EstadoCompra>(estado, true, out var estadoEnum))
            {
                query = query.Where(c => c.Estado == estadoEnum);
            }

            // 2. Filtro por Usuario (quien registró la compra)
            if (idUsuario.HasValue && idUsuario > 0)
            {
                query = query.Where(c => c.IdUsuario == idUsuario);
            }

            // 3. Filtro por Rango de Fechas
            if (fechaInicio.HasValue)
            {
                query = query.Where(c => c.FechaCompra >= fechaInicio.Value);
            }

            if (fechaFin.HasValue)
            {
                var finDia = fechaFin.Value.Date.AddDays(1).AddTicks(-1);
                query = query.Where(c => c.FechaCompra <= finDia);
            }

            return await query.ToListAsync();
        }


        public async Task<(IEnumerable<MovimientoStock> Items, int Total)> GetMovimientosPagedAsync(
        int page,
        int pageSize,
        string? tipo,
        string? origen)
        {
            var query = _context.MovimientosStock
                .AsNoTracking()
                .Include(m => m.Producto)
                .AsQueryable();

            // Filtro por Tipo (Enum)
            if (!string.IsNullOrEmpty(tipo) && Enum.TryParse<TipoMovimiento>(tipo, true, out var tipoEnum))
            {
                query = query.Where(m => m.Tipo == tipoEnum);
            }

            // Filtro por Origen (Enum)
            if (!string.IsNullOrEmpty(origen) && Enum.TryParse<OrigenMovimiento>(origen, true, out var origenEnum))
            {
                query = query.Where(m => m.Origen == origenEnum);
            }

            // Ordenamiento por defecto: Fecha descendente (Lo más nuevo primero)
            query = query.OrderByDescending(m => m.Fecha);

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

        public async Task<List<Pago>> GetPagosAsync(string? estado)
        {
            var query = _context.Pagos
                .OrderByDescending(p => p.FechaPago)
                .AsQueryable();

            if (!string.IsNullOrEmpty(estado))
                query = query.Where(p => p.EstadoPago.ToString() == estado);

            return await query.ToListAsync();
        }

        public async Task<List<Comprobante>> GetComprobantesAsync(string? estado)
        {
            var query = _context.Comprobantes
                .OrderByDescending(c => c.FechaEmision)
                .AsQueryable();

            if (!string.IsNullOrEmpty(estado))
                query = query.Where(c => c.Estado.ToString() == estado);

            return await query.ToListAsync();
        }
    }
}
