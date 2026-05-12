using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class Pago
    {
        public int IdPago { get; set; }

        public int IdVenta { get; set; }
        public Venta Venta { get; set; } = null!;

        public MetodoPago MetodoPago { get; set; }
        public decimal Monto { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public DateTime FechaPago { get; set; }
    }
    public enum MetodoPago { EFECTIVO, TARJETA, YAPE }
    public enum EstadoPago { PAGADO, PENDIENTE, CANCELADO }


}
