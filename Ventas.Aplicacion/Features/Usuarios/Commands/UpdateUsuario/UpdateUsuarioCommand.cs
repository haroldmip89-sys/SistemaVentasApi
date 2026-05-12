using MediatR;
using Ventas.Aplicacion.Features.Usuarios.DTOs;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.UpdateUsuario
{
    public class UpdateUsuarioCommand : IRequest<UpdateUsuarioResponseDTO>
    {
        public int IdUsuario { get; }
        public string Nombre { get; }
        public string Email { get; }

        public UpdateUsuarioCommand(int idUsuario, string nombre, string email)
        {
            IdUsuario = idUsuario;
            Nombre = nombre;
            Email = email;
        }
    }
}
