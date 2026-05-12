using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Infraestructura.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // 1. Crear el mensaje con MimeKit
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_config["SmtpSettings:SenderName"], _config["SmtpSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            // 2. Construir el cuerpo (HTML)
            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };
            message.Body = bodyBuilder.ToMessageBody();

            // 3. Enviar con MailKit SMTP Client
            using var client = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                // 1. Determinar el puerto y la opción de seguridad
                int port = int.Parse(_config["SmtpSettings:Port"]);

                // Si el puerto es 465, usamos SslOnConnect (SSL puro)
                // Si es 587, usamos StartTls
                var securityOption = port == 465
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.StartTls;

                // 2. Añadir un Timeout explícito corto para no bloquear el Handler demasiado tiempo
                client.Timeout = 15000; // 15 segundos

                await client.ConnectAsync(
                    _config["SmtpSettings:Server"],
                    port,
                    securityOption
                );

                await client.AuthenticateAsync(_config["SmtpSettings:SenderEmail"], _config["SmtpSettings:Password"]);
                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico en EmailService: {ex.Message}");
                // No lanzamos la excepción aquí si no queremos que rompa el flujo del Handler,
                // pero como tu Handler ya tiene un try-catch, está bien hacer el throw.
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
