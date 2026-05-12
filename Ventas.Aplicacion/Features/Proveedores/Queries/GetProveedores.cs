using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Proveedores.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Proveedores.Queries
{
    public record GetProveedoresQuery()
    : IRequest<IEnumerable<ProveedorResponseDTO>>;

    public class GetProveedoresHandler
    : IRequestHandler<GetProveedoresQuery, IEnumerable<ProveedorResponseDTO>>
    {
        private readonly IProveedorRepository _repo;

        public GetProveedoresHandler(IProveedorRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ProveedorResponseDTO>> Handle(
            GetProveedoresQuery request,
            CancellationToken cancellationToken)
        {
            var proveedores = await _repo.GetProveedoresAsync();

            return proveedores.Select(p => new ProveedorResponseDTO
            {
                IdProveedor = p.IdProveedor,
                RazonSocial = p.RazonSocial,
                RUC = p.RUC,
                Telefono = p.Telefono,
                Email = p.Email,
                Estado = p.Estado
            });
        }
    }
}
