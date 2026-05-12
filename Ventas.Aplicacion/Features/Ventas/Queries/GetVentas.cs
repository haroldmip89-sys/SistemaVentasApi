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
    public record GetVentasQuery(
    string? Estado,
    DateTime? FechaInicio = null,
    DateTime? FechaFin = null,
    int? IdUsuario = null
) : IRequest<List<VentaResponseDTO>>;

    public class GetVentasHandler
        : IRequestHandler<GetVentasQuery, List<VentaResponseDTO>>
    {
        private readonly IConsultaRepository _repo;

        public GetVentasHandler(IConsultaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<VentaResponseDTO>> Handle(
            GetVentasQuery request,
            CancellationToken cancellationToken)
        {
            var ventas = await _repo.GetVentasAsync(
                request.Estado,
                request.FechaInicio,
                request.FechaFin,
                request.IdUsuario
            );

            return ventas.Select(v => new VentaResponseDTO
            {
                IdVenta = v.IdVenta,
                ClienteNombre = v.ClienteNombre,
                Total = v.Total,
                Estado = v.EstadoVenta.ToString(),
                FechaVenta = v.FechaVenta,
                Usuario = v.Usuario.Nombre,
                Comprobante = v.Comprobante.ToString() ?? string.Empty, //boleta o Factura
                // USAR ComprobanteElectronico en lugar de Comprobante
                NumeroComprobante = (v.ComprobanteElectronico != null &&
                         !string.IsNullOrEmpty(v.ComprobanteElectronico.Serie) &&
                         !string.IsNullOrEmpty(v.ComprobanteElectronico.Numero))
        ? $"{v.ComprobanteElectronico.Serie}-{v.ComprobanteElectronico.Numero}"
        : null,

                Detalles = v.Detalles.Select(d => new DetalleVentaResponseDTO
                {
                    IdProducto = d.IdProducto,
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()

            }).ToList();
        }
    }
}
