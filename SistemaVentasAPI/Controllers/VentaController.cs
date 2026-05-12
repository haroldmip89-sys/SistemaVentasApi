using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventas.Aplicacion.Features.Compras.CrearCompra;
using Ventas.Aplicacion.Features.Compras.Queries;
using Ventas.Aplicacion.Features.Ventas.AnularVenta;
using Ventas.Aplicacion.Features.Ventas.CrearVenta;
using Ventas.Aplicacion.Features.Ventas.DTOs;
using Ventas.Aplicacion.Features.Ventas.EmitirComprobante;
using Ventas.Aplicacion.Features.Ventas.Queries;
using Ventas.Aplicacion.Features.Ventas.ReenviarComprobante;
using Ventas.Aplicacion.Features.Ventas.RegistrarPago;

namespace SistemaVentasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VentaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("venta")]
        public async Task<IActionResult> CrearVenta([FromBody] CrearVentaCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return Ok(new { IdVenta = id });
        }

        [HttpPost("compra")]
        public async Task<IActionResult> CrearCompra([FromBody] CrearCompraCommand cmd)
        {
            var id = await _mediator.Send(cmd);
            return Ok(new { IdCompra = id });
        }

        [HttpPost("pago")]
        public async Task<IActionResult> RegistrarPago([FromBody] RegistrarPagoCommand cmd)
        {
            await _mediator.Send(cmd);
            return NoContent();
        }

        [HttpPost("comprobante")]
        public async Task<IActionResult> EmitirComprobante([FromBody] EmitirComprobanteCommand cmd)
        {
            await _mediator.Send(cmd);
            return NoContent();
        }
        [HttpPatch("reenviar")]
        public async Task<IActionResult> Reenviar([FromBody] ReenviarComprobanteCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(new { success = result, message = $"Reenviado a {cmd.NuevoEmail}" });
        }

        [HttpPost("anular/{id}")]
        public async Task<IActionResult> AnularVenta(int id)
        {
            await _mediator.Send(new AnularVentaCommand(id));
            return NoContent();
        }

        //VENTAS POR ID
        [HttpGet("venta/{id}")]
        public async Task<IActionResult> GetVentaById(int id)
        {
            var query = new GetVentaByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // VENTAS
        [HttpGet("ventas")]
        public async Task<IActionResult> GetVentas(
            [FromQuery] string? estado,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] int? idUsuario)
        {
            var query = new GetVentasQuery(estado, fechaInicio, fechaFin, idUsuario);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // COMPRAS
        [HttpGet("compras")]
        public async Task<IActionResult> GetCompras(
            [FromQuery] string? estado,
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin,
            [FromQuery] int? idUsuario)
        {
            var query = new GetComprasQuery(estado, fechaInicio, fechaFin, idUsuario);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        //  MOVIMIENTOS
        [HttpGet("movimientos")]
        public async Task<IActionResult> GetMovimientos(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] string? tipo = null,//"ENTRADA" o "SALIDA".
            [FromQuery] string? origen = null //"COMPRA", "VENTA" o "ANULACION"
        )
        {
            var result = await _mediator.Send(new GetMovimientosPagedQuery(page, pageSize, tipo, origen));
            return Ok(result);
        }

        //  PAGOS
        [HttpGet("pagos")]
        public async Task<IActionResult> GetPagos([FromQuery] string? estado)
        {
            var result = await _mediator.Send(new GetPagosQuery(estado));
            return Ok(result);
        }

        //  COMPROBANTES
        [HttpGet("comprobantes")]
        public async Task<IActionResult> GetComprobantes([FromQuery] string? estado)
        {
            var result = await _mediator.Send(new GetComprobantesQuery(estado));
            return Ok(result);
        }
    }
}
