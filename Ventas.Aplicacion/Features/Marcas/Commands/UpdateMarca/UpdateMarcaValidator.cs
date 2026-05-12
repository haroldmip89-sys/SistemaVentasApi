using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Marcas.Commands.UpdateMarca
{
    public class UpdateMarcaValidator : AbstractValidator<UpdateMarcaCommand>
    {
        public UpdateMarcaValidator()
        {
            RuleFor(x => x.IdMarca)
                .GreaterThan(0)
                .WithMessage("IdMarca inválido.");

            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre de la marca es requerido.")
                .MaximumLength(100);

            RuleFor(x => x.Descripcion)
                .MaximumLength(250);

            RuleFor(x => x.ColorHex)
                .Matches("^#([A-Fa-f0-9]{6})$")
                .When(x => !string.IsNullOrEmpty(x.ColorHex))
                .WithMessage("El color debe estar en formato HEX.");
        }
    }
}
