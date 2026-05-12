using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Productos.DTOs;

namespace Ventas.Aplicacion.Features.Productos.Queries.GetProductoById
{
    public record GetProductoByIdQuery(int IdProducto)
    : IRequest<ProductoResponseDTO?>;
}
