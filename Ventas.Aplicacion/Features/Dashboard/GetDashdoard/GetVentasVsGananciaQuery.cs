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
    public record GetVentasVsGananciaQuery(int Dias) : IRequest<List<VentasPorDiaDTO>>;

    public class GetVentasVsGananciaHandler : IRequestHandler<GetVentasVsGananciaQuery, List<VentasPorDiaDTO>>
    {
        private readonly IDashboardRepository _repository;
        public GetVentasVsGananciaHandler(IDashboardRepository repository) => _repository = repository;

        public async Task<List<VentasPorDiaDTO>> Handle(GetVentasVsGananciaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetVentasVsGananciaAsync(request.Dias);
        }
    }
}
