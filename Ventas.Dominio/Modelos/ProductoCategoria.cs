using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class ProductoCategoria
    {
        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;

        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; } = null!;
    }

}
