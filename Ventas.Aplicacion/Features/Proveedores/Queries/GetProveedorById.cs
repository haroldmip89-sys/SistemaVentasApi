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
    public record GetProveedorByIdQuery(int Id)
        : IRequest<ProveedorResponseDTO?>;
    public class GetProveedorByIdHandler
    : IRequestHandler<GetProveedorByIdQuery, ProveedorResponseDTO?>
    {
        private readonly IProveedorRepository _repo;

        public GetProveedorByIdHandler(IProveedorRepository repo)
        {
            _repo = repo;
        }

        public async Task<ProveedorResponseDTO?> Handle(
            GetProveedorByIdQuery request,
            CancellationToken cancellationToken)
        {
            var p = await _repo.GetProveedorByIdAsync(request.Id);

            if (p == null) return null;

            return new ProveedorResponseDTO
            {
                IdProveedor = p.IdProveedor,
                RazonSocial = p.RazonSocial,
                RUC = p.RUC,
                Telefono = p.Telefono,
                Email = p.Email,
                Estado = p.Estado
            };
        }
    }
}
