using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Features.Usuarios.DTOs;
using Ventas.Aplicacion.Common.Exceptions;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.UpdateUsuario
{
    public class UpdateUsuarioHandler
    : IRequestHandler<UpdateUsuarioCommand, UpdateUsuarioResponseDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UpdateUsuarioHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UpdateUsuarioResponseDTO> Handle(
            UpdateUsuarioCommand request,
            CancellationToken cancellationToken)
        {
            //validar si existe el usuario
            var usuario = await _usuarioRepository.GetUsuarioByIdAsync(request.IdUsuario);

            if (usuario is null)
                throw new BadRequestException("Usuario no encontrado");

            usuario.Nombre = request.Nombre;
            usuario.Email = request.Email;

            var updated = await _usuarioRepository.UpdateUsuarioAsync(usuario);

            return new UpdateUsuarioResponseDTO
            {
                IdUsuario = updated.IdUsuario,
                Nombre = updated.Nombre,
                Email = updated.Email
            };
        }
    }
}
