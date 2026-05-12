using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Interfaces
{
    public interface IConsultaRepository
    {
        Task<Venta?> GetVentaByIdAsync(int idVenta); // Nuevo método
        Task<List<Venta>> GetVentasAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, int? idUsuario);
        Task<List<Compra>> GetComprasAsync(string? estado, DateTime? fechaInicio, DateTime? fechaFin, int? idUsuario);
        Task<(IEnumerable<MovimientoStock> Items, int Total)> GetMovimientosPagedAsync(int page,int pageSize,string? tipo,string? origen);
        Task<List<Pago>> GetPagosAsync(string? estado);
        Task<List<Comprobante>> GetComprobantesAsync(string? estado);
    }
}
