using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public RolUsuario Rol { get; set; }
        public bool Estado { get; set; }

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
        public ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
    public enum RolUsuario
    {
        ADMIN,
        VENTAS,
        INVENTARIO
    }

}
