using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Modelos
{
    public class Marca
    {
        public int IdMarca { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? ColorHex { get; set; }
        public bool Estado { get; set; }

        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }

}
