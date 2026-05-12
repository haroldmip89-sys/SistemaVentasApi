using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Productos.Commands.DeleteProducto
{
    public class DeleteProductoHandler
        : IRequestHandler<DeleteProductoCommand, bool>
    {
        private readonly IProductoRepository _repository;

        public DeleteProductoHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            DeleteProductoCommand request,
            CancellationToken cancellationToken)
        {
            return await _repository
                .DeleteProductoAsync(request.IdProducto);
        }
    }
}
