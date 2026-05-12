using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Services
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
