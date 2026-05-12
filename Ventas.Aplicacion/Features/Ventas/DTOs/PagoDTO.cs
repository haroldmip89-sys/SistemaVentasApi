using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Ventas.DTOs
{
    public class PagoDTO
    {
        public int IdPago { get; set; }
        public int IdVenta { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateTime FechaPago { get; set; }
    }
}
