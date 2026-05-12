using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Usuarios.Commands.Login
{
    public class LoginValidator:AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email no puede estar vacío.")
                .EmailAddress().WithMessage("El email no es válido.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña no puede estar vacía.")
                .MinimumLength(3).WithMessage("La contraseña debe tener al menos 3 caracteres.");
        }
    }
}
