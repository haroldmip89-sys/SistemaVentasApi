using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Features.Usuarios.DTOs;

namespace Ventas.Aplicacion.Features.Usuarios.Queries.GetUsuarios
{
    public class GetUsuariosQueryHandler
        : IRequestHandler<GetUsuariosQuery, IEnumerable<UsuarioResponseDTO>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuariosQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioResponseDTO>> Handle(
            GetUsuariosQuery request,
            CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.GetUsuariosAsync();

            return usuarios.Select(u => new UsuarioResponseDTO
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Email = u.Email,
                Rol = u.Rol.ToString(),
                Estado = u.Estado
            });
        }
    }
}
