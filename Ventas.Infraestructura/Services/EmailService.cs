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
                // En Vercel/Producción usamos SecureSocketOptions.StartTls para el puerto 587
                await client.ConnectAsync(
                    _config["SmtpSettings:Server"],
                    int.Parse(_config["SmtpSettings:Port"]),
                    SecureSocketOptions.StartTls
                );

                // Autenticación con la contraseña de aplicación de Google
                await client.AuthenticateAsync(_config["SmtpSettings:SenderEmail"], _config["SmtpSettings:Password"]);

                await client.SendAsync(message);
            }
            catch (Exception ex)
            {
                // Loguear el error (importante para depurar en Vercel)
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                throw;
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
