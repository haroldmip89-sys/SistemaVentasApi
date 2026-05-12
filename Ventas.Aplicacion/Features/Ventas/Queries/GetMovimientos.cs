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
    public record GetMovimientosPagedQuery(
    int Page,
    int PageSize,
    string? Tipo,
    string? Origen
    ) : IRequest<MovimientoPagedResponse>;

    // DTO de respuesta para incluir metadatos de paginación
    public record MovimientoPagedResponse(
        IEnumerable<MovimientoStockDTO> Movimientos,
        int Total,
        int Page,
        int PageSize
    );
    //HANDLER
    public class GetMovimientosHandler
    : IRequestHandler<GetMovimientosPagedQuery, MovimientoPagedResponse>
    {
        private readonly IConsultaRepository _repo;

        public GetMovimientosHandler(IConsultaRepository repo)
        {
            _repo = repo;
        }

        public async Task<MovimientoPagedResponse> Handle(GetMovimientosPagedQuery request, CancellationToken cancellationToken)
        {
            var (items, total) = await _repo.GetMovimientosPagedAsync(
                request.Page,
                request.PageSize,
                request.Tipo,
                request.Origen);

            var dtos = items.Select(m => new MovimientoStockDTO
            {
                IdMovimiento = m.IdMovimiento,
                Producto = m.Producto.Nombre,
                Tipo = m.Tipo.ToString(),
                Origen = m.Origen.ToString(),
                Cantidad = m.Cantidad,
                Fecha = m.Fecha
            });

            return new MovimientoPagedResponse(dtos, total, request.Page, request.PageSize);
        }
    }
}
