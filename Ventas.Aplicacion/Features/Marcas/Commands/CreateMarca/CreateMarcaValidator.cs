using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Marcas.Commands.CreateMarca
{
    public class CreateMarcaValidator : AbstractValidator<CreateMarcaCommand>
    {
        public CreateMarcaValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre de la marca es requerido.")
                .MaximumLength(100)
                .WithMessage("El nombre no puede tener más de 100 caracteres.");

            RuleFor(x => x.Descripcion)
                .MaximumLength(250)
                .WithMessage("La descripción no puede tener más de 250 caracteres.");

            RuleFor(x => x.ColorHex)
                .Matches("^#([A-Fa-f0-9]{6})$")
                .When(x => !string.IsNullOrEmpty(x.ColorHex))
                .WithMessage("El color debe estar en formato HEX. Ej: #FF5733");
        }
    }
}
