using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.UpdateUsuario
{
    public class UpdateUsuarioValidator
    : AbstractValidator<UpdateUsuarioCommand>
    {
        public UpdateUsuarioValidator()
        {
            RuleFor(x => x.IdUsuario)
                .GreaterThan(0);

            RuleFor(x => x.Nombre)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
