using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Ventas.DTOs
{
    public class MovimientoStockDTO
    {
        public int IdMovimiento { get; set; }
        public string Producto { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public string Origen { get; set; } = null!;
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
    }
}
