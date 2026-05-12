using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Productos.DTOs;

namespace Ventas.Aplicacion.Features.Productos.Commands.CreateProducto
{
    public record CreateProductoCommand
    (
     string Nombre,
     string? Descripcion,
     string? Imagen,
     int IdMarca,
     decimal PrecioVenta,
     List<int> Categorias
    ):IRequest<CreateProductoResponseDTO>;
}
