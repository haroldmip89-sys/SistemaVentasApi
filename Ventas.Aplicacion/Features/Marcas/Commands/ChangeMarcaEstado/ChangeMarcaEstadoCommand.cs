using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Marcas.Commands.ChangeMarcaEstado
{
    public record ChangeMarcaEstadoCommand(
    int IdMarca,
    bool Estado
) : IRequest<bool>;
}
