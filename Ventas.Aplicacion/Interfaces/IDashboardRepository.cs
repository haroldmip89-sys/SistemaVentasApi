using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Dashboard.DTOs;

namespace Ventas.Aplicacion.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardKpiDTO> GetKpisAsync(int dias);
        Task<List<VentasPorDiaDTO>> GetVentasVsGananciaAsync(int dias);
        Task<List<TopProductoDTO>> GetTopProductosAsync(int dias);
        Task<List<TopProductoDTO>> GetTopProductosPorFechaAsync(DateTime fecha);
        Task<List<CategoriaVentaDTO>> GetVentasPorCategoriaAsync();
        Task<List<FechaDTO>> GetFechasAsync();
    }
}
