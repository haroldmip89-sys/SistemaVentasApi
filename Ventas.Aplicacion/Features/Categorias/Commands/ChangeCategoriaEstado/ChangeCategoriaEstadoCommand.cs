using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Categorias.Commands.ChangeCategoriaEstado
{
    public record ChangeCategoriaEstadoCommand(
    int IdCategoria,
    bool Estado
) : IRequest<bool>;
}
