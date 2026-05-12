using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Categorias.DTOs;

namespace Ventas.Aplicacion.Features.Categorias.Commands.CreateCategoria
{
    public record CreateCategoriaCommand(
    string Nombre,
    string? Descripcion,
    string? ColorHex
) : IRequest<int>;
}
