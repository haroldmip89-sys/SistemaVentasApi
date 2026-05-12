using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Usuarios.DTOs;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.UpdateEstadoUsuario
{
    public class UpdateEstadoUsuarioCommand : IRequest<UpdateEstadoUsuarioResponseDTO>
    {
        public int IdUsuario { get; }
        public bool Estado { get; }

        public UpdateEstadoUsuarioCommand(int idUsuario, bool estado)
        {
            IdUsuario = idUsuario;
            Estado = estado;
        }
    }
}
