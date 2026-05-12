using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Marcas.Commands.UpdateMarca
{
    public record UpdateMarcaCommand(
    int IdMarca,
    string Nombre,
    string? Descripcion,
    string? ColorHex
) : IRequest<bool>;
}
