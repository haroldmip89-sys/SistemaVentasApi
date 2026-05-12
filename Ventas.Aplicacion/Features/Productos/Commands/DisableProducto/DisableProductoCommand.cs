using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Productos.Commands.DisableProducto
{
    public record DisableProductoCommand(int IdProducto, bool Estado)
    : IRequest<bool>;
}
