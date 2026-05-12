using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class Venta
    {
        public int IdVenta { get; set; }

        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public TipoComprobante Comprobante { get; set; }
        public TipoDocumento? TipoDocumento { get; set; }
        public string? NumeroDocumento { get; set; }

        public string ClienteNombre { get; set; } = null!;
        public string? ClienteEmail { get; set; }

        public decimal Total { get; set; }
        public EstadoVenta EstadoVenta { get; set; }
        public DateTime FechaVenta { get; set; }

        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
        public Comprobante? ComprobanteElectronico { get; set; }
    }
    public enum EstadoVenta { REGISTRADA, PAGADA, ANULADA }
    public enum TipoComprobante { BOLETA, FACTURA }
    public enum TipoDocumento { DNI, RUC }


}
