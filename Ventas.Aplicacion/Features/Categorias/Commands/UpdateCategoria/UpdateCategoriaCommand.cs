using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Categorias.Commands.UpdateCategoria
{
    public record UpdateCategoriaCommand(
    int IdCategoria,
    string Nombre,
    string? Descripcion,
    string? ColorHex
) : IRequest<bool>;
}
