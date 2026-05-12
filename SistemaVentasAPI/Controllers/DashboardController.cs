using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventas.Aplicacion.Features.Dashboard.DTOs;
using Ventas.Aplicacion.Features.Dashboard.GetDashdoard;


namespace SistemaVentasAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("kpis")]
        public async Task<ActionResult<DashboardKpiDTO>> GetKpis([FromQuery] int dias = 7)
        {
            return Ok(await _mediator.Send(new GetKpisQuery(dias)));
        }

        [HttpGet("ventas-vs-ganancia")]
        public async Task<ActionResult<List<VentasPorDiaDTO>>> GetVentasVsGanancia([FromQuery] int dias = 7)
        {
            return Ok(await _mediator.Send(new GetVentasVsGananciaQuery(dias)));
        }

        [HttpGet("top-productos")]
        public async Task<ActionResult<List<TopProductoDTO>>> GetTopProductos([FromQuery] int dias = 30)
        {
            return Ok(await _mediator.Send(new GetTopProductosQuery(dias)));
        }

        [HttpGet("top-productos-fecha")]
        public async Task<ActionResult<List<TopProductoDTO>>> GetTopProductosPorFecha([FromQuery] DateTime fecha)
        {
            // Si la fecha viene vacía, podrías usar DateTime.Today
            return Ok(await _mediator.Send(new GetTopProductosPorFechaQuery(fecha)));
        }

        [HttpGet("categorias")]
        public async Task<ActionResult<List<CategoriaVentaDTO>>> GetCategorias()
        {
            return Ok(await _mediator.Send(new GetVentasPorCategoriaQuery()));
        }

        [HttpGet("fechas")]
        public async Task<ActionResult<List<FechaDTO>>> GetFechas()
        {
            return Ok(await _mediator.Send(new GetFechasQuery()));
        }

        // OPCIONAL: Un endpoint que traiga TODO el dashboard de un solo golpe
        [HttpGet("full")]
        public async Task<ActionResult<DashboardResponseDTO>> GetFullDashboard([FromQuery] int dias = 7)
        {
            var response = new DashboardResponseDTO
            {
                Kpis = await _mediator.Send(new GetKpisQuery(dias)),
                VentasVsGanancia = await _mediator.Send(new GetVentasVsGananciaQuery(dias)),
                TopProductos = await _mediator.Send(new GetTopProductosQuery(dias)),
                VentasPorCategoria = await _mediator.Send(new GetVentasPorCategoriaQuery()),
                FechasDisponibles = await _mediator.Send(new GetFechasQuery())
            };

            return Ok(response);
        }
    }
}
