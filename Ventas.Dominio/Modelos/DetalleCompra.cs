using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class DetalleCompra
    {
        public int IdDetalleCompra { get; set; }

        public int IdCompra { get; set; }
        public Compra Compra { get; set; } = null!;

        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal PrecioCompra { get; set; }
    }

}
