using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Productos.Commands.DisableProducto
{
    public class DisableProductoHandler
    : IRequestHandler<DisableProductoCommand, bool>
    {
        private readonly IProductoRepository _repository;

        public DisableProductoHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            DisableProductoCommand request,
            CancellationToken cancellationToken)
        {
            var producto = await _repository
                .GetProductoByIdAsync(request.IdProducto);

            if (producto == null)
                throw new Exception("Producto no encontrado.");

            producto.Estado = request.Estado;

            await _repository.UpdateProductoAsync(producto);

            return true;
        }
    }
}
