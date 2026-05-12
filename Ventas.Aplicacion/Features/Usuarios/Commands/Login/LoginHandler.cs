using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Usuarios.DTOs;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Services;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponseDTO>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginHandler(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _tokenService = tokenService;
        }
        public async Task<LoginResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)//Email, PasswordHash
        {
            //consultar el usuario en la base de datos
            var usuario = await _usuarioRepository.LoginAsync(
                request.Email,
                request.Password
            );

            //verificar si el usuario es null
            if (usuario == null || !usuario.Estado)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            //generar el token
            var token = _tokenService.CreateToken(usuario);

            //retornar el LoginResponseDTO
            return new LoginResponseDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Rol = usuario.Rol.ToString(),
                Token = token
            };
        }
    }
}
