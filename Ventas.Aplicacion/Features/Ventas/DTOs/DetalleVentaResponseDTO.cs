using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Ventas.DTOs
{
    public class DetalleVentaResponseDTO
    {
        public int IdProducto { get; set; }
        public string Producto { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
