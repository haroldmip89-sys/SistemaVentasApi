using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class MovimientoStock
    {
        public int IdMovimiento { get; set; }

        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;

        public TipoMovimiento Tipo { get; set; }
        public OrigenMovimiento Origen { get; set; }

        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdReferencia { get; set; }
    }
    public enum TipoMovimiento { ENTRADA, SALIDA }
    public enum OrigenMovimiento { COMPRA, VENTA, ANULACION }


}
