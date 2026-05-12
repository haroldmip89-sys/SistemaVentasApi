using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Dashboard.DTOs
{
    public class DashboardKpiDTO
    {
        public decimal TotalVentas { get; set; }
        public int CantidadVentas { get; set; }
        public decimal TotalPendiente { get; set; }
        public int CantidadPendiente { get; set; }
        public decimal VentasHoy { get; set; }
        public int CantidadVentasHoy { get; set; }
        public decimal TicketPromedio { get; set; }
        public decimal Rentabilidad { get; set; }
    }
}
