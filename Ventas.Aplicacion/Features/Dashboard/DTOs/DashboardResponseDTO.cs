using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Dashboard.DTOs
{
    public class DashboardResponseDTO
    {
        public DashboardKpiDTO Kpis { get; set; } = new();

        public List<VentasPorDiaDTO> VentasVsGanancia { get; set; } = new();

        public List<TopProductoDTO> TopProductos { get; set; } = new();

        public List<TopProductoDTO> TopProductosPorFecha { get; set; } = new();

        public List<CategoriaVentaDTO> VentasPorCategoria { get; set; } = new();

        public List<FechaDTO> FechasDisponibles { get; set; } = new();
    }
}
