using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Productos.DTOs;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Features.Productos.Queries.GetProductosPaged
{
    public record GetProductosPagedQuery(
    int Page = 1,
    int PageSize = 12,
    string? Search = null,
    int? idCategoria = null,
    int? idMarca = null,
    string? OrderBy = "nombre",
    string? OrderDir = "asc",
    bool soloActivos = true
) : IRequest<PagedResultResponseDTO<ProductoResponseDTO>>;
}
