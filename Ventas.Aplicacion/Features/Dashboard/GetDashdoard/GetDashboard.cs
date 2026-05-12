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
    public record GetKpisQuery(int Dias) : IRequest<DashboardKpiDTO>;

    public class GetKpisHandler : IRequestHandler<GetKpisQuery, DashboardKpiDTO>
    {
        private readonly IDashboardRepository _repository;
        public GetKpisHandler(IDashboardRepository repository) => _repository = repository;

        public async Task<DashboardKpiDTO> Handle(GetKpisQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetKpisAsync(request.Dias);
        }
    }
}
