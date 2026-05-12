using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Ventas.RegistrarPago
{
    public record RegistrarPagoCommand(
    int IdVenta,
    string MetodoPago,
    decimal Monto
) : IRequest<Unit>;

    public class RegistrarPagoHandler : IRequestHandler<RegistrarPagoCommand, Unit>
    {
        private readonly IStoredProcedureExecutor _sp;

        public RegistrarPagoHandler(IStoredProcedureExecutor sp)
        {
            _sp = sp;
        }

        public async Task<Unit> Handle(RegistrarPagoCommand request, CancellationToken cancellationToken)
        {
            await _sp.RegistrarPagoAsync(request.IdVenta, request.MetodoPago, request.Monto);
            return Unit.Value;
        }
    }
}
