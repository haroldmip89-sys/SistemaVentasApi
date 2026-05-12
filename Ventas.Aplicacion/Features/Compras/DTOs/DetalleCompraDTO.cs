using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Compras.DTOs
{
    public class DetalleCompraDTO
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
    }
}
