using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Features.Ventas.Utils
{
    public static class ComprobanteTemplate
    {
        public static string GenerarHtml(Venta venta, string serie, string numero, DateTime fechaEmision)
        {
            string fechaFormateada = fechaEmision.ToString("dd/MM/yyyy HH:mm");
            //https://res.cloudinary.com/dxdquydwl/image/upload/v1778212145/logo_blanco_v1_ttsv8y.png
            string logoUrl = "https://res.cloudinary.com/dxdquydwl/image/upload/v1778227102/logo-light-v2_egbgqb.png";
            var filas = new StringBuilder();
            foreach (var det in venta.Detalles)
            {
                filas.Append($@"
            <tr>
                <td style='padding: 14px 12px; border-bottom: 1px solid #f1f5f9; font-size: 14px; color: #1e293b;'>{det.Producto?.Nombre ?? "Producto"}</td>
                    <td style='padding: 14px 12px; border-bottom: 1px solid #f1f5f9; text-align: center; font-size: 14px; color: #475569;'>{det.Cantidad}</td>
                    <td style='padding: 14px 12px; border-bottom: 1px solid #f1f5f9; text-align: right; font-size: 14px; color: #475569;'>S/ {det.PrecioUnitario:N2}</td>
                    <td style='padding: 14px 12px; border-bottom: 1px solid #f1f5f9; text-align: right; font-size: 14px; font-weight: 700; color: #0f172a;'>S/ {(det.Cantidad * det.PrecioUnitario):N2}</td>
            </tr>");
            }

            return $@"
            <div style='background-color: #f3f4f6; padding: 20px 0; font-family: ""Segoe UI"", Arial, sans-serif;'>
                <div style='max-width: 600px; margin: 0 auto; border: 1px solid #e2e8f0; border-radius: 16px; overflow: hidden; background-color: white;'>
                    
                    <!-- Encabezado Oscuro con Logo a la Izquierda -->
                    <div style='background-color: #111827; padding: 30px;'>
                        <table width='100%' border='0' cellpadding='0' cellspacing='0'>
                            <tr>
                                <td width='80' style='vertical-align: middle;'>
                                    {(!string.IsNullOrEmpty(logoUrl) ? $"<img src='{logoUrl}' alt='Logo' style='width: 70px; height: 70px; object-fit: contain; display: block;' />" : "")}
                                </td>
                                <td style='padding-left: 20px; vertical-align: middle; text-align: left;'>
                                    <h1 style='margin: 0; font-size: 20px; font-weight: 800; color: white; text-transform: uppercase; letter-spacing: -0.025em;'>¡Gracias por tu compra!</h1>
                                    <p style='margin: 4px 0 0; color: #9ca3af; font-size: 13px; font-weight: 500;'>{venta.Comprobante} Electrónica {serie}-{numero}</p>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div style='padding: 40px 30px;'>
                        <table width='100%' border='0' cellpadding='0' cellspacing='0' style='margin-bottom: 30px;'>
                            <tr>
                                <td style='text-align: left;'>
                                    <p style='margin: 0; color: #64748b; font-size: 11px; text-transform: uppercase; font-weight: 700; letter-spacing: 0.05em;'>Cliente</p>
                                    <p style='margin: 4px 0 0; color: #0f172a; font-weight: 700; font-size: 17px;'>{venta.ClienteNombre ?? "Cliente General"}</p>
                                </td>
                                <td style='text-align: right;'>
                                    <p style='margin: 0; color: #64748b; font-size: 11px; text-transform: uppercase; font-weight: 700; letter-spacing: 0.05em;'>Fecha de Emisión</p>
                                    <p style='margin: 4px 0 0; color: #0f172a; font-weight: 600; font-size: 15px;'>{fechaFormateada}</p>
                                </td>
                            </tr>
                        </table>

                        <p style='color: #475569; font-size: 14px; line-height: 1.6; margin-bottom: 25px;'>
                            Tu transacción en <strong>Casa del Play</strong> ha sido confirmada. Aquí tienes el detalle de los artículos adquiridos:
                        </p>

                        <table width='100%' border='0' cellpadding='0' cellspacing='0' style='margin-bottom: 30px;'>
                            <thead>
                                <tr style='background-color: #f8fafc; color: #64748b; font-size: 11px; text-transform: uppercase;'>
                                    <th style='padding: 12px; text-align: left; border-bottom: 2px solid #f1f5f9;'>Descripción</th>
                                    <th style='padding: 12px; text-align: center; border-bottom: 2px solid #f1f5f9;'>Cant.</th>
                                    <th style='padding: 12px; text-align: right; border-bottom: 2px solid #f1f5f9;'>Unit.</th>
                                    <th style='padding: 12px; text-align: right; border-bottom: 2px solid #f1f5f9;'>Total</th>
                                </tr>
                            </thead>
                            <tbody>{filas}</tbody>
                        </table>

                        <div style='border-top: 2px solid #f1f5f9; padding-top: 20px; text-align: right;'>
                            <span style='color: #64748b; font-size: 13px; font-weight: 600;'>TOTAL PAGADO:</span>
                            <div style='color: #111827; font-size: 32px; font-weight: 800; margin-top: 5px;'>S/ {venta.Total:N2}</div>
                        </div>
                    </div>

                    <div style='background-color: #f9fafb; padding: 25px; text-align: center; border-top: 1px solid #f1f5f9;'>
                        <p style='margin: 0; font-weight: 700; color: #1f2937; font-size: 13px; text-transform: uppercase; letter-spacing: 0.1em;'>Casa del Play</p>
                        <p style='margin: 6px 0 0; color: #9ca3af; font-size: 11px;'>Lima, Perú • Documento autorizado por SUNAT</p>
                        <div style='margin-top: 12px; border-top: 1px solid #e5e7eb; padding-top: 12px;'>
                            <p style='margin: 0; color: #6b7280; font-size: 10px; font-style: italic;'>Este es un comprobante automático, por favor no responda a este correo.</p>
                        </div>
                    </div>
                </div>
            </div>";
        }
    }
}
