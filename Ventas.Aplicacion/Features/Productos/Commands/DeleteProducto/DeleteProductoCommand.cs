using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Productos.Commands.DeleteProducto
{
    public record DeleteProductoCommand(int IdProducto)
    : IRequest<bool>;
}
