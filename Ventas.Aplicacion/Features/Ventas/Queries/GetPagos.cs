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
    public record GetPagosQuery(string? Estado)
    : IRequest<List<PagoDTO>>;
    public class GetPagosHandler
    : IRequestHandler<GetPagosQuery, List<PagoDTO>>
    {
        private readonly IConsultaRepository _repo;

        public GetPagosHandler(IConsultaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<PagoDTO>> Handle(
            GetPagosQuery request,
            CancellationToken cancellationToken)
        {
            var pagos = await _repo.GetPagosAsync(request.Estado);

            return pagos.Select(p => new PagoDTO
            {
                IdPago = p.IdPago,
                IdVenta = p.IdVenta,
                Monto = p.Monto,
                MetodoPago = p.MetodoPago.ToString(),
                Estado = p.EstadoPago.ToString(),
                FechaPago = p.FechaPago
            }).ToList();
        }
    }
}
