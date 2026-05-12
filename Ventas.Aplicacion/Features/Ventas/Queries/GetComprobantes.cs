using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Ventas.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Ventas.Queries
{
    public record GetComprobantesQuery(string? Estado)
    : IRequest<List<ComprobanteDTO>>;
    public class GetComprobantesHandler
    : IRequestHandler<GetComprobantesQuery, List<ComprobanteDTO>>
    {
        private readonly IConsultaRepository _repo;

        public GetComprobantesHandler(IConsultaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ComprobanteDTO>> Handle(
            GetComprobantesQuery request,
            CancellationToken cancellationToken)
        {
            var comprobantes = await _repo.GetComprobantesAsync(request.Estado);

            return comprobantes.Select(c => new ComprobanteDTO
            {
                IdComprobante = c.IdComprobante,
                IdVenta = c.IdVenta,
                Tipo = c.Tipo.ToString(),
                Serie = c.Serie ?? "",
                Numero = c.Numero ?? "",
                Estado = c.Estado.ToString(),
                EmailDestino= c.EmailDestino ?? "",
                FechaEmision = c.FechaEmision
            }).ToList();
        }
    }
}
