using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }

        public int IdVenta { get; set; }
        public Venta Venta { get; set; } = null!;

        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

}
