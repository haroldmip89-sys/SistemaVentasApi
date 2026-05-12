using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class Comprobante
    {
        public int IdComprobante { get; set; }

        public int IdVenta { get; set; }
        public Venta Venta { get; set; } = null!;

        public TipoComprobante Tipo { get; set; }
        public string? Serie { get; set; }
        public string? Numero { get; set; }
        public EstadoComprobante Estado { get; set; }
        public string? EmailDestino { get; set; }
        public DateTime FechaEmision { get; set; }
    }
    public enum EstadoComprobante
    {
        EMITIDO,
        ANULADO,
        NO_ENVIADO,
        ENVIADO
    }


}
