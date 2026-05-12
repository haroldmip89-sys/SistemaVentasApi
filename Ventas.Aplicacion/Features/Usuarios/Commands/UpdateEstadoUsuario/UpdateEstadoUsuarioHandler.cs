using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Usuarios.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.UpdateEstadoUsuario
{
    public class UpdateEstadoUsuarioHandler
        : IRequestHandler<UpdateEstadoUsuarioCommand, UpdateEstadoUsuarioResponseDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UpdateEstadoUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UpdateEstadoUsuarioResponseDTO> Handle(
            UpdateEstadoUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(request.IdUsuario);

            if (usuario is null)
                throw new KeyNotFoundException("Usuario no encontrado");

            usuario.Estado = request.Estado;

            await _usuarioRepository.UpdateUsuarioAsync(usuario);

            return new UpdateEstadoUsuarioResponseDTO
            {
                IdUsuario = usuario.IdUsuario,
                Estado = usuario.Estado,
                Mensaje = usuario.Estado
                    ? "Usuario activado correctamente"
                    : "Usuario desactivado correctamente"
            };
        }
    }
}
