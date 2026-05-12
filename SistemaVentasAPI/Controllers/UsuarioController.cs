using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVentasAPI.DTOs.Usuario;
using Ventas.Aplicacion.Features.Usuarios.Commands.Login;
using Ventas.Aplicacion.Features.Usuarios.Commands.UpdateUsuario;
using Ventas.Aplicacion.Features.Usuarios.Queries.GetUsuarios;


namespace SistemaVentasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        [AllowAnonymous]
        //Login Principal
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var response = await _mediator.Send(
                new LoginCommand(
                    request.Email,
                    request.Password
                ));
            return Ok(response);
        }
        ////Crear Venta -ADMIN,VENTAS
        ////[Authorize(Roles = "ADMIN,VENTAS")]
        //[HttpPost("venta")]
        //public IActionResult CrearVenta()
        //{
        //    return Ok();
        //}
        ////Crear Producto -INVENTARIO
        ////[Authorize(Roles = "INVENTARIO")]
        //[HttpPost("producto")]
        //public IActionResult CrearProducto()
        //{
        //    return Ok();
        //}
        //Actualizar Usuario -TODOS
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id,[FromBody] UpdateUsuarioRequestDTO request)
        {
            var response = await _mediator.Send(
                new UpdateUsuarioCommand(
                    id,
                    request.Nombre,
                    request.Email
                ));

            return Ok(response);
        }
        //Actulizar Estado Usuario -ADMIN
        [HttpPut("estado/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ActualizarEstadoUsuario(int id, [FromBody] UpdateEstadoUsuarioRequestDTO request)
        {
            var response = await _mediator.Send(
                new Ventas.Aplicacion.Features.Usuarios.Commands.UpdateEstadoUsuario.UpdateEstadoUsuarioCommand(
                    id,
                    request.Estado
                ));
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var result = await _mediator.Send(new GetUsuariosQuery());
            return Ok(result);
        }
    }
}
