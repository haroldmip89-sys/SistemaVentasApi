using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Categorias.DTOs;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Features.Productos.DTOs
{
    public class ProductoResponseDTO
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }

        public decimal PrecioVenta { get; set; }
        public decimal CostoPromedio { get; set; } // <--- Añadir esto
        public int StockActual { get; set; }
        public bool Estado { get; set; }

        // Marca “plana”, no toda la entidad
        public int IdMarca { get; set; }
        public string Marca { get; set; } = null!;
        //  Categorías del producto
        public List<CategoriaResponseDTO> Categorias { get; set; } = new();
    }
}
