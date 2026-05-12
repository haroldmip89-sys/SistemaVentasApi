using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Ventas.DTOs
{
    public class ComprobanteDTO
    {
        public int IdComprobante { get; set; }
        public int IdVenta { get; set; }
        public string Tipo { get; set; } = null!;
        public string Serie { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string EmailDestino { get; set; } = null!;
        public DateTime FechaEmision { get; set; }
    }
}
