using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentasAPI.DTOs.Producto;
using Ventas.Aplicacion.Common.Interfaces;
using Ventas.Aplicacion.Features.Productos.Commands.CreateProducto;
using Ventas.Aplicacion.Features.Productos.Commands.DeleteProducto;
using Ventas.Aplicacion.Features.Productos.Commands.DisableProducto;
using Ventas.Aplicacion.Features.Productos.Commands.UpdateProducto;
using Ventas.Aplicacion.Features.Productos.Queries.GetProductoById;
using Ventas.Aplicacion.Features.Productos.Queries.GetProductosPaged;

namespace SistemaVentasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IImageStorageService _imageStorageService;

        public ProductoController(IMediator mediator, IImageStorageService imageStorageService)
        {
            _mediator = mediator;
            _imageStorageService = imageStorageService;

        }
        // GET: api/Producto
        [HttpGet]
        public async Task<IActionResult> GetProductos(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 12,
            [FromQuery] string? search = null,
            [FromQuery] int? idCategoria = null,
            [FromQuery] int? idMarca = null,
            [FromQuery] string? orderBy = "nombre",
            [FromQuery] string? orderDir = "asc",
            [FromQuery] bool soloActivos = true)
        {
            var result = await _mediator.Send(
                new GetProductosPagedQuery(
                    page,
                    pageSize,
                    search,
                    idCategoria,
                    idMarca,
                    orderBy,
                    orderDir,
                    soloActivos
                ));

            return Ok(result);
        }

        // GET: api/Producto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProducto(int id)
        {
            var result = await _mediator.Send(new GetProductoByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Producto
        [HttpPost]
        public async Task<IActionResult> CreateProducto(
        [FromForm] CreateProductoFormDTO dto)
        {
            string? imagenUrl = null;

            if (dto.Imagen != null)
            {
                imagenUrl = await _imageStorageService
                    .SaveImageAsync(dto.Imagen, "productos");
            }

            var command = new CreateProductoCommand(
                dto.Nombre,
                dto.Descripcion,
                imagenUrl,
                dto.IdMarca,
                dto.PrecioVenta,
                dto.Categorias
            );

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdProducto), new { id = result.IdProducto }, result);
        }

        // PATCH: api/Producto/disable/5
        [HttpPatch("disable/{id}")]
        public async Task<IActionResult> ChangeEstado(
        int id,
        [FromBody] ChangeEstadoRequestDTO request)
        {
            var result = await _mediator.Send(
                new DisableProductoCommand(id, request.Estado)
            );

            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var result = await _mediator.Send(new DeleteProductoCommand(id));

            if (!result)
                return NotFound();

            return NoContent();
        }

        // PUT: api/Producto/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(
            int id,
            [FromForm] UpdateProductoFormDTO dto)
        {
            string? imagenUrl = null;
            if (dto.Imagen != null)
            {
                imagenUrl = await _imageStorageService
                    .SaveImageAsync(dto.Imagen, "productos");
            }
            var command = new UpdateProductoCommand(
                id,
                dto.Nombre,
                dto.Descripcion,
                imagenUrl,
                dto.IdMarca,
                dto.PrecioVenta,
                dto.Categorias
            );
            var result = await _mediator.Send(command);
            if (result == null)
                return NotFound();
            return Ok(result);

        }
    }
}
