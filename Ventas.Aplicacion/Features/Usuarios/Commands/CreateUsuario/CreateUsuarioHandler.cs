using MediatR;
using Ventas.Aplicacion.Common.Exceptions;
using Ventas.Aplicacion.Features.Usuarios.DTOs;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.CreateUsuario
{
    public class CreateUsuarioHandler: IRequestHandler<CreateUsuarioCommand, CreateUsuarioResponseDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public CreateUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<CreateUsuarioResponseDTO> Handle(CreateUsuarioCommand request, CancellationToken cancellationToken)
        {
            //validar si existe el correo existe
            var emailExiste = await _usuarioRepository.EmailExistsAsync(request.Email);

            if (emailExiste)
            {
                throw new BadRequestException("El correo ya está registrado");
            }
            var usuario = new Usuario
            {
                Nombre = request.Nombre,
                Email = request.Email,
                PasswordHash = request.Password,
                Rol = request.Rol,
                Estado = true
            };

            var usuarioCreado = await _usuarioRepository.AddUsuarioAsync(usuario);
            return new CreateUsuarioResponseDTO
            {
                IdUsuario = usuarioCreado.IdUsuario,
                Nombre = usuarioCreado.Nombre,
                Email = usuarioCreado.Email,
                Estado = usuarioCreado.Estado
            };
        }
    }
}
