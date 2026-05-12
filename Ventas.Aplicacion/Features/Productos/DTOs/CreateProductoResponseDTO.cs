using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Productos.DTOs
{
    public class CreateProductoResponseDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
       public string? Descripcion { get; set; }
       
       public int IdMarca { get; set; }
       public decimal PrecioVenta { get; set; }
       public List<int> Categorias { get; set; } = null!;
    }
}
