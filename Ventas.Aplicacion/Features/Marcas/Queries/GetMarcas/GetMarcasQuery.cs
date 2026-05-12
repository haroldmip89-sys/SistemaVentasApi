using MediatR;
using Ventas.Aplicacion.Features.Marcas.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Marcas.Queries.GetMarcas
{
    public record GetMarcasQuery()
    : IRequest<IEnumerable<MarcaResponseDTO>>;
}
