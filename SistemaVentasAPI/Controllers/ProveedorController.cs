using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ventas.Aplicacion.Features.Proveedores.Commands;
using Ventas.Aplicacion.Features.Proveedores.Queries;
using Ventas.Aplicacion.Features.Proveedores.DTOs;

namespace SistemaVentasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProveedorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetProveedoresQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetProveedorByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] ProveedorRequestDTO request)
        {
            var id = await _mediator.Send(new CreateProveedorCommand(
                request.RazonSocial,
                request.RUC,
                request.Telefono,
                request.Email
            ));

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ProveedorRequestDTO request)
        {
            var result = await _mediator.Send(new UpdateProveedorCommand(
                id,
                request.RazonSocial,
                request.RUC,
                request.Telefono,
                request.Email
            ));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProveedorCommand(id));

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
