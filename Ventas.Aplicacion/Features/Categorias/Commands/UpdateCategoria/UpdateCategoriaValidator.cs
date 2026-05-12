using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Categorias.Commands.UpdateCategoria
{
    public class UpdateCategoriaValidator : AbstractValidator<UpdateCategoriaCommand>
    {
        public UpdateCategoriaValidator()
        {
            RuleFor(x => x.IdCategoria)
                .GreaterThan(0)
                .WithMessage("IdCategoria inválido.");

            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre de la categoría es requerido.")
                .MaximumLength(100);

            RuleFor(x => x.Descripcion)
                .MaximumLength(250);

            RuleFor(x => x.ColorHex)
                .Matches("^#([A-Fa-f0-9]{6})$")
                .When(x => !string.IsNullOrEmpty(x.ColorHex));
        }
    }
}
