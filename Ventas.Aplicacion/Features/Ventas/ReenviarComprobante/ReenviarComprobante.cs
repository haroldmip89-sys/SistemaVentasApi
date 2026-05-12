using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Ventas.Utils;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Ventas.ReenviarComprobante
{
    public record ReenviarComprobanteCommand : IRequest<bool>
    {
        public int IdVenta { get; init; }
        [JsonPropertyName("email")]
        public string NuevoEmail { get; init; }=null!;
    };
    public class ReenviarComprobanteHandler : IRequestHandler<ReenviarComprobanteCommand, bool>
    {
        private readonly IConsultaRepository _repo;
        private readonly IEmailService _emailService; // Tu servicio de correos

        public ReenviarComprobanteHandler(IConsultaRepository repo, IEmailService emailService)
        {
            _repo = repo;
            _emailService = emailService;
        }
        public async Task<bool> Handle(ReenviarComprobanteCommand request, CancellationToken ct)
        {
            // 1. Obtener la venta
            var venta = await _repo.GetVentaByIdAsync(request.IdVenta);

            if (venta == null)
                throw new Exception($"La venta con ID {request.IdVenta} no existe en la base de datos.");

            // 2. VALIDACIÓN CRÍTICA: ¿Viene el objeto ComprobanteElectronico?
            
            if (venta.ComprobanteElectronico == null)
            {
                // Si es null, intentamos usar las propiedades planas si existen en 'venta'
                // Por ejemplo: venta.Serie y venta.Numero
                throw new Exception("El objeto ComprobanteElectronico es NULL. Revisa si la consulta SQL trae los datos del comprobante.");
            }

            // 3. VALIDACIÓN DEL CORREO
            // Si venta.ClienteEmail es null, el correo nunca saldrá.
            string destinatario = venta.ComprobanteElectronico.EmailDestino;

            if (string.IsNullOrWhiteSpace(destinatario))
                throw new Exception("El comprobante no tiene un correo electrónico de destino registrado.");
            // -------------------

            try
            {
                // 4. Generar HTML
                string htmlContent = ComprobanteTemplate.GenerarHtml(
                    venta,
                    venta.ComprobanteElectronico.Serie,
                    venta.ComprobanteElectronico.Numero,
                    venta.ComprobanteElectronico.FechaEmision
                );

                string asunto = $"Reenvío: {venta.Comprobante} Electrónica {venta.ComprobanteElectronico.Serie}-{venta.ComprobanteElectronico.Numero}";

                // 5. Enviar Correo
                await _emailService.SendEmailAsync(request.NuevoEmail, asunto, htmlContent);
                return true;
            }
            catch (Exception ex)
            {
                // Esto aparecerá en tu consola de Visual Studio (Output)
                Console.WriteLine($"FALLO EN REENVIO: {ex.Message}");
                throw; // Re-lanzamos para que Swagger/Frontend vean el error 500
            }
        }
    }
}
