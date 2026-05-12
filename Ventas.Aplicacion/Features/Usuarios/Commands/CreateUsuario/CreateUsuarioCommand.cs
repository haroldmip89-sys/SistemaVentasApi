using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Usuarios.DTOs;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.CreateUsuario
{
    public record CreateUsuarioCommand(
        string Nombre,
        string Email,
        string Password,
        RolUsuario Rol
    ) : IRequest<CreateUsuarioResponseDTO>;
}
