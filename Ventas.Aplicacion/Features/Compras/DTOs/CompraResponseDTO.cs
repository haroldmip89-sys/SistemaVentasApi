using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Compras.DTOs
{
    public class CompraResponseDTO
    {
        public int IdCompra { get; set; }
        public string Proveedor { get; set; } = null!;
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime FechaCompra { get; set; }
        public string Usuario { get; set; } = null!;

        public List<DetalleCompraResponseDTO> Detalles { get; set; } = new();
    }
}
