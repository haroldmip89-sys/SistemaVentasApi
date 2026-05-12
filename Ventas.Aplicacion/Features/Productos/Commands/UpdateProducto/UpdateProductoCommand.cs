using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Productos.DTOs;

namespace Ventas.Aplicacion.Features.Productos.Commands.UpdateProducto
{
    public record UpdateProductoCommand
    (
     int IdProducto,
     string Nombre,
     string? Descripcion,
     string? Imagen,
     int IdMarca,
     decimal PrecioVenta,
     List<int> Categorias
    ) : IRequest<UpdateProductoResponseDTO>;
}
