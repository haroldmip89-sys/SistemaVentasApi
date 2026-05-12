using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Marcas.DTOs;

namespace Ventas.Aplicacion.Features.Marcas.Queries.GetMarcaById
{
    public record GetMarcaByIdQuery(int IdMarca) : IRequest<MarcaResponseDTO?>;
   
}
