using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentasAPI.DTOs.Marca;
using Ventas.Aplicacion.Features.Marcas.Commands.ChangeMarcaEstado;
using Ventas.Aplicacion.Features.Marcas.Commands.CreateMarca;
using Ventas.Aplicacion.Features.Marcas.Queries.GetMarcaById;
using Ventas.Aplicacion.Features.Marcas.Commands.UpdateMarca;
using Ventas.Aplicacion.Features.Marcas.Queries.GetMarcas;


namespace SistemaVentasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MarcaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //Change marca estado (actualiza solo estado soft delete)
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> ChangeEstadoMarca(
         int id,
         bool estado)
        {
            var result = await _mediator.Send(
                new ChangeMarcaEstadoCommand(id, estado)
            );

            if (!result)
                return NotFound();

            return NoContent();
        }

        //Create marca
        [HttpPost]
        public async Task<IActionResult> CreateMarca(
            [FromBody] CreateMarcaRequestDTO request)
        {
            var idMarca = await _mediator.Send(
                new CreateMarcaCommand(
                    request.Nombre,
                    request.Descripcion,
                    request.ColorHex
                ));
            return CreatedAtAction(
                nameof(GetByIdMarca),
                new { id = idMarca },
                null);
        }

        //Get marca by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMarca(int id)
        {
            var result = await _mediator.Send(new GetMarcaByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //update marca
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMarca(
            int id,
            [FromBody] UpdateMarcaRequestDTO request)
        {
            var result = await _mediator.Send(
                new UpdateMarcaCommand(
                    id,
                    request.Nombre,
                    request.Descripcion,
                    request.ColorHex
                ));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //get all
        [HttpGet]
        public async Task<IActionResult> GetAllMarcas()
        {
            var result = await _mediator.Send(new GetMarcasQuery());
            return Ok(result);

        }
    }
}
