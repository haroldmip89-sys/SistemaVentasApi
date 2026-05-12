using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Marcas.Commands.ChangeMarcaEstado
{
    public class ChangeMarcaEstadoValidator : AbstractValidator<ChangeMarcaEstadoCommand>
    {
        public ChangeMarcaEstadoValidator()
        {
            RuleFor(x => x.IdMarca)
                .GreaterThan(0)
                .WithMessage("IdMarca inválido.");
        }
    }
}
