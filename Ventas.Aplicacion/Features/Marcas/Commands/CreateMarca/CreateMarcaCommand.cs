using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Marcas.Commands.CreateMarca
{
    public record CreateMarcaCommand(
    string Nombre,
    string? Descripcion,
    string? ColorHex
) : IRequest<int>;
}
