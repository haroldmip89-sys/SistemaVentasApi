using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Dashboard.DTOs
{
    public class VentasPorDiaDTO
    {
        public DateTime Dia { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal Ganancia { get; set; }
        public decimal Margen { get; set; }
    }
}
