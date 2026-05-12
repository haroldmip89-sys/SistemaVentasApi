using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.UpdateEstadoUsuario
{
    public class UpdateEstadoUsuarioValidator
        : AbstractValidator<UpdateEstadoUsuarioCommand>
    {
        public UpdateEstadoUsuarioValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0)
                .WithMessage("El id del usuario es inválido");
        }
    }
}
