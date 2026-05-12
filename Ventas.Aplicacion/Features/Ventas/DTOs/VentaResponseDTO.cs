using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Ventas.DTOs
{
    public class VentaResponseDTO
    {
        public int IdVenta { get; set; }
        public string ClienteNombre { get; set; } = null!;
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime FechaVenta { get; set; }
        public string Usuario { get; set; } = null!;
        public string Comprobante { get; set; } = null!;
        public string? NumeroComprobante { get; set; }
        public List<DetalleVentaResponseDTO> Detalles { get; set; } = new();
    }
}
