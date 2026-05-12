using MediatR;
using Microsoft.AspNetCore.Mvc;
using SistemaVentasAPI.DTOs.Categoria;
using Ventas.Aplicacion.Features.Categorias.Commands.ChangeCategoriaEstado;
using Ventas.Aplicacion.Features.Categorias.Commands.CreateCategoria;
using Ventas.Aplicacion.Features.Categorias.Commands.UpdateCategoria;
using Ventas.Aplicacion.Features.Categorias.Queries.GetCategoriaById;
using Ventas.Aplicacion.Features.Categorias.Queries.GetCategorias;

namespace SistemaVentasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Change estado (soft delete / activar)
        [HttpPatch("estado/{id}")]
        public async Task<IActionResult> ChangeEstadoCategoria(
            int id,
            bool estado)
        {
            var result = await _mediator.Send(
                new ChangeCategoriaEstadoCommand(id, estado)
            );

            if (!result)
                return NotFound();

            return NoContent();
        }

        // Create categoria
        [HttpPost]
        public async Task<IActionResult> CreateCategoria(
            [FromBody] CreateCategoriaRequestDTO request)
        {
            var idCategoria = await _mediator.Send(
                new CreateCategoriaCommand(
                    request.Nombre,
                    request.Descripcion,
                    request.ColorHex
                )
            );

            return CreatedAtAction(
                nameof(GetByIdCategoria),
                new { id = idCategoria },
                null
            );
        }

        // Get categoria by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategoria(int id)
        {
            var result = await _mediator.Send(new GetCategoriaByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // Update categoria
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoria(
            int id,
            [FromBody] UpdateCategoriaRequestDTO request)
        {
            var result = await _mediator.Send(
                new UpdateCategoriaCommand(
                    id,
                    request.Nombre,
                    request.Descripcion,
                    request.ColorHex
                )
            );

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // Get all categorias
        [HttpGet]
        public async Task<IActionResult> GetAllCategorias()
        {
            var result = await _mediator.Send(new GetCategoriasQuery());
            return Ok(result);
        }
    }
}