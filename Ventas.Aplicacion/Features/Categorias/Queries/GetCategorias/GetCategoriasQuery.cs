using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ventas.Aplicacion.Features.Categorias.DTOs;

namespace Ventas.Aplicacion.Features.Categorias.Queries.GetCategorias
{
    public record GetCategoriasQuery() : IRequest<IEnumerable<CategoriaResponseDTO>>;
}
