using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Ventas.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Ventas.CrearVenta
{
    public record CrearVentaCommand(
    int IdUsuario,
    string Comprobante,
    string? TipoDocumento,
    string? NumeroDocumento,
    string? ClienteNombre,
    string? ClienteEmail,
    List<DetalleVentaDTO> Detalles
) : IRequest<int>;

    public class CrearVentaHandler : IRequestHandler<CrearVentaCommand, int>
    {
        private readonly IStoredProcedureExecutor _sp;

        public CrearVentaHandler(IStoredProcedureExecutor sp)
        {
            _sp = sp;
        }
        public async Task<int> Handle(CrearVentaCommand request, CancellationToken cancellationToken)
        {
            return await _sp.CrearVentaAsync(
                request.IdUsuario,
                request.Comprobante,
                request.TipoDocumento,
                request.NumeroDocumento,
                request.ClienteNombre,
                request.ClienteEmail,
                request.Detalles
            );
        }

        //public async Task<int> Handle(CrearVentaCommand request, CancellationToken cancellationToken)
        //{
        //    var table = new DataTable();
        //    table.Columns.Add("IdProducto", typeof(int));
        //    table.Columns.Add("Cantidad", typeof(int));

        //    foreach (var d in request.Detalles)
        //        table.Rows.Add(d.IdProducto, d.Cantidad);

        //    return await _sp.CrearVentaAsync(
        //        request.IdUsuario,
        //        request.Comprobante,
        //        request.TipoDocumento,
        //        request.NumeroDocumento,
        //        request.ClienteNombre,
        //        request.ClienteEmail,
        //        table
        //    );
        //}
    }
}
