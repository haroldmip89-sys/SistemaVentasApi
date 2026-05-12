using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Imagen { get; set; }

        public int IdMarca { get; set; }
        public Marca Marca { get; set; } = null!;

        public decimal PrecioVenta { get; set; }
        public decimal CostoPromedio { get; set; }
        public int StockActual { get; set; }
        public bool Estado { get; set; }

        public ICollection<ProductoCategoria> Categorias { get; set; } = new List<ProductoCategoria>();
        public ICollection<DetalleVenta> DetallesVenta { get; set; } = new List<DetalleVenta>();
        public ICollection<DetalleCompra> DetallesCompra { get; set; } = new List<DetalleCompra>();
        public ICollection<MovimientoStock> Movimientos { get; set; } = new List<MovimientoStock>();
    }

}
