using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class Compra
    {
        public int IdCompra { get; set; }

        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; } = null!;

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public decimal Total { get; set; }
        public DateTime FechaCompra { get; set; }
        public EstadoCompra Estado { get; set; }

        public ICollection<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();
    }
    public enum EstadoCompra
    {
        REGISTRADA,
        ANULADA
    }


}
