using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Proveedores.DTOs
{
    public class ProveedorResponseDTO
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; } = null!;
        public string RUC { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public bool Estado { get; set; }
    }
}
