using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Compras.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Compras.Queries
{
    public record GetComprasQuery(
        string? Estado,
        DateTime? FechaInicio = null,
        DateTime? FechaFin = null,
        int? IdUsuario = null
    ) : IRequest<List<CompraResponseDTO>>;
    public class GetComprasHandler
    : IRequestHandler<GetComprasQuery, List<CompraResponseDTO>>
    {
        private readonly IConsultaRepository _repo;

        public GetComprasHandler(IConsultaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CompraResponseDTO>> Handle(
            GetComprasQuery request,
            CancellationToken cancellationToken)
        {
            var compras = await _repo.GetComprasAsync(
                request.Estado,
                request.FechaInicio,
                request.FechaFin,
                request.IdUsuario
            );

            return compras.Select(c => new CompraResponseDTO
            {
                IdCompra = c.IdCompra,
                Proveedor = c.Proveedor.RazonSocial,
                Total = c.Total,
                Estado = c.Estado.ToString(),
                FechaCompra = c.FechaCompra,
                Usuario = c.Usuario.Nombre,

                Detalles = c.Detalles.Select(d => new DetalleCompraResponseDTO
                {
                    IdProducto = d.IdProducto,
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioCompra = d.PrecioCompra
                }).ToList()

            }).ToList();
        }
    }
}
