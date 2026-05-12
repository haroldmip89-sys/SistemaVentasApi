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
    public record GetTopProductosQuery(int Dias) : IRequest<List<TopProductoDTO>>;

    public class GetTopProductosHandler : IRequestHandler<GetTopProductosQuery, List<TopProductoDTO>>
    {
        private readonly IDashboardRepository _repository;
        public GetTopProductosHandler(IDashboardRepository repository) => _repository = repository;

        public async Task<List<TopProductoDTO>> Handle(GetTopProductosQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetTopProductosAsync(request.Dias);
        }
    }
}
