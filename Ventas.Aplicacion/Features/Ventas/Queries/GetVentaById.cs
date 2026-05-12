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
    public record GetVentaByIdQuery(int IdVenta) : IRequest<VentaResponseDTO>;

    public class GetVentaByIdHandler : IRequestHandler<GetVentaByIdQuery, VentaResponseDTO>
    {
        private readonly IConsultaRepository _repo;
        public GetVentaByIdHandler(IConsultaRepository repo)
        {
            _repo = repo;
        }
        public async Task<VentaResponseDTO> Handle(
            GetVentaByIdQuery request,
            CancellationToken cancellationToken)
        {
            var venta = await _repo.GetVentaByIdAsync(request.IdVenta);
            if (venta == null)
                throw new Exception("Venta no encontrada");
            return new VentaResponseDTO
            {
                IdVenta = venta.IdVenta,
                ClienteNombre = venta.ClienteNombre,
                Total = venta.Total,
                Estado = venta.EstadoVenta.ToString(),
                FechaVenta = venta.FechaVenta,
                Usuario = venta.Usuario.Nombre,
                Comprobante = venta.Comprobante.ToString() ?? string.Empty, //boleta o Factura
                // USAR ComprobanteElectronico en lugar de Comprobante
                NumeroComprobante = (venta.ComprobanteElectronico != null &&
                         !string.IsNullOrEmpty(venta.ComprobanteElectronico.Serie) &&
                         !string.IsNullOrEmpty(venta.ComprobanteElectronico.Numero))
        ? $"{venta.ComprobanteElectronico.Serie}-{venta.ComprobanteElectronico.Numero}"
        : null,
                Detalles = venta.Detalles.Select(d => new DetalleVentaResponseDTO
                {
                    IdProducto = d.IdProducto,
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                }).ToList()
            };
        }
    }
}
