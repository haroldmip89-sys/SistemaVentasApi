using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Ventas.Utils;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Ventas.EmitirComprobante
{
    public record EmitirComprobanteCommand(
    int IdVenta,
    string? Email
) : IRequest<Unit>;

    public class EmitirComprobanteHandler : IRequestHandler<EmitirComprobanteCommand, Unit>
    {
        private readonly IStoredProcedureExecutor _sp;
        private readonly IEmailService _emailService;
        private readonly IConsultaRepository _consultaRepo;

        public EmitirComprobanteHandler(
            IStoredProcedureExecutor sp,
            IEmailService emailService,
            IConsultaRepository consultaRepo)
        {
            _sp = sp;
            _emailService = emailService;
            _consultaRepo = consultaRepo;
        }

        public async Task<Unit> Handle(EmitirComprobanteCommand request, CancellationToken cancellationToken)
        {
            string? emailFinal = string.IsNullOrWhiteSpace(request.Email) ? null : request.Email;
            await _sp.EmitirComprobanteAsync(
                request.IdVenta,
                emailFinal
            );
            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                try
                {
                    var venta = await _consultaRepo.GetVentaByIdAsync(request.IdVenta);
                    if (venta != null)
                    {
                        // REUTILIZACIÓN DE TEMPLATE
                        string htmlContent = ComprobanteTemplate.GenerarHtml(venta, 
                            venta.ComprobanteElectronico.Serie,
                            venta.ComprobanteElectronico.Numero, 
                            venta.ComprobanteElectronico.FechaEmision);
                        string asunto = $"{venta.Comprobante} Electrónica {venta.ComprobanteElectronico.Serie}-{venta.ComprobanteElectronico.Numero}";

                        await _emailService.SendEmailAsync(request.Email, asunto, htmlContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al enviar correo: {ex.Message}");
                }
            }

            return Unit.Value;
        }
        
    }
}
