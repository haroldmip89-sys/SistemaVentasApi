using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Dashboard.DTOs
{
    public class TopProductoDTO
    {
        public string Nombre { get; set; } = null!;
        public decimal Ingresos { get; set; }
        public decimal Ganancia { get; set; }
        public int TotalVendido { get; set; }
        public decimal Rentabilidad { get; set; }
    }
}
