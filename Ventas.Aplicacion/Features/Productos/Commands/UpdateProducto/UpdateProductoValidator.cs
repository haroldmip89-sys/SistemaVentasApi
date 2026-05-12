using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ventas.Aplicacion.Features.Productos.Commands.UpdateProducto
{
    public class UpdateProductoValidator : AbstractValidator<UpdateProductoCommand>
    {
        public UpdateProductoValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre del producto es requerido.");
            RuleFor(x => x.PrecioVenta).GreaterThan(0).WithMessage("El precio de venta debe ser mayor que cero.");
            RuleFor(x => x.IdMarca).GreaterThan(0).WithMessage("La marca es requerida.");
            RuleFor(x => x.Categorias).NotEmpty().WithMessage("Al menos una categoría es requerida.");
        }
    }
}