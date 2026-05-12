using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Ventas.AnularVenta
{
    public record AnularVentaCommand(int IdVenta) : IRequest<Unit>;

    public class AnularVentaHandler : IRequestHandler<AnularVentaCommand, Unit>
    {
        private readonly IStoredProcedureExecutor _sp;

        public AnularVentaHandler(IStoredProcedureExecutor sp)
        {
            _sp = sp;
        }

        public async Task<Unit> Handle(AnularVentaCommand request, CancellationToken cancellationToken)
        {
            await _sp.AnularVentaAsync(request.IdVenta);
            return Unit.Value;
        }
    }
}
