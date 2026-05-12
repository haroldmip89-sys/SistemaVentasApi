using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Compras.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Compras.CrearCompra
{
    public record CrearCompraCommand(
    int IdProveedor,
    int IdUsuario,
    List<DetalleCompraDTO> Detalles
) : IRequest<int>;

    public class CrearCompraHandler : IRequestHandler<CrearCompraCommand, int>
    {
        private readonly IStoredProcedureExecutor _sp;

        public CrearCompraHandler(IStoredProcedureExecutor sp)
        {
            _sp = sp;
        }

        public async Task<int> Handle(CrearCompraCommand request, CancellationToken cancellationToken)
        {
            return await _sp.CrearCompraAsync(
                request.IdProveedor,
                request.IdUsuario,
                request.Detalles
            );
        }

        //public async Task<int> Handle(CrearCompraCommand request, CancellationToken cancellationToken)
        //{
        //    var table = new DataTable();
        //    table.Columns.Add("IdProducto", typeof(int));
        //    table.Columns.Add("Cantidad", typeof(int));
        //    table.Columns.Add("PrecioCompra", typeof(decimal));

        //    foreach (var d in request.Detalles)
        //        table.Rows.Add(d.IdProducto, d.Cantidad, d.PrecioCompra);

        //    return await _sp.CrearCompraAsync(request.IdProveedor, request.IdUsuario, table);
        //}
    }
}
