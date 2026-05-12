using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Usuarios.DTOs
{
    public class UpdateEstadoUsuarioResponseDTO
    {
        public int IdUsuario { get; set; }
        public bool Estado { get; set; }
        public string Mensaje { get; set; } = string.Empty;
    }
}
