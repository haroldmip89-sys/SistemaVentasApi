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
    public record GetVentasPorCategoriaQuery() : IRequest<List<CategoriaVentaDTO>>;
    public class GetVentasPorCategoriaHandler : IRequestHandler<GetVentasPorCategoriaQuery, List<CategoriaVentaDTO>>
    {
        private readonly IDashboardRepository _repository;
        public GetVentasPorCategoriaHandler(IDashboardRepository repository) => _repository = repository;
        public async Task<List<CategoriaVentaDTO>> Handle(GetVentasPorCategoriaQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetVentasPorCategoriaAsync();
        }
    }
}
