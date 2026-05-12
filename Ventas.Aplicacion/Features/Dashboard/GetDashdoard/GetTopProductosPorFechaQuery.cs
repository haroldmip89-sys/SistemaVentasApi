using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Dashboard.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Dashboard.GetDashdoard
{
    public record GetTopProductosPorFechaQuery(DateTime Fecha) : IRequest<List<TopProductoDTO>>;

    public class GetTopProductosPorFechaHandler : IRequestHandler<GetTopProductosPorFechaQuery, List<TopProductoDTO>>
    {
        private readonly IDashboardRepository _repository;
        public GetTopProductosPorFechaHandler(IDashboardRepository repository) => _repository = repository;

        public async Task<List<TopProductoDTO>> Handle(GetTopProductosPorFechaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetTopProductosPorFechaAsync(request.Fecha);
        }
    }
}
