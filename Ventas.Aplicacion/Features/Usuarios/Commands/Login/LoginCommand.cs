using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Usuarios.DTOs;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.Login
{
    public record LoginCommand(
        string Email,
        string Password
    ) : IRequest<LoginResponseDTO>;
}
