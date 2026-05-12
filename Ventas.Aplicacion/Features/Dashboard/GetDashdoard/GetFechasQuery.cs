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
    public record GetFechasQuery() : IRequest<List<FechaDTO>>;
    public class GetFechasHandler : IRequestHandler<GetFechasQuery, List<FechaDTO>>
    {
        private readonly IDashboardRepository _repository;
        public GetFechasHandler(IDashboardRepository repository) => _repository = repository;
        public async Task<List<FechaDTO>> Handle(GetFechasQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetFechasAsync();
        }
    }
}
