using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Ventas.DTOs;
using Ventas.Aplicacion.Features.Compras.DTOs;

namespace Ventas.Aplicacion.Interfaces
{
    public interface IStoredProcedureExecutor
    {
        Task<int> CrearCompraAsync(int idProveedor, int idUsuario, List<DetalleCompraDTO> detalles);
        Task<int> CrearVentaAsync(
            int idUsuario,
            string comprobante,
            string? tipoDocumento,
            string? numeroDocumento,
            string? clienteNombre,
            string? clienteEmail,
            List<DetalleVentaDTO> detalles);

        Task RegistrarPagoAsync(int idVenta, string metodoPago, decimal monto);
        Task EmitirComprobanteAsync(int idVenta, string? email);
        Task AnularVentaAsync(int idVenta);
    }
}
