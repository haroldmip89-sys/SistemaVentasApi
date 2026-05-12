using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.CreateUsuario
{
    public class CreateUsuarioValidator : AbstractValidator<CreateUsuarioCommand>
    {
        public CreateUsuarioValidator() 
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email no puede estar vacío.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña no puede estar vacía.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
            RuleFor(x => x.Rol)
                .NotEmpty().WithMessage("El rol no puede estar vacío.");
        }
    }
}
