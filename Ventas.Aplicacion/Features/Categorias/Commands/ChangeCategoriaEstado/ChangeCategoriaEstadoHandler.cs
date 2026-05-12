using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Categorias.Commands.ChangeCategoriaEstado
{
    public class ChangeCategoriaEstadoHandler : IRequestHandler<ChangeCategoriaEstadoCommand, bool>
    {
        private readonly ICategoriaRepository _repository;
        public ChangeCategoriaEstadoHandler(ICategoriaRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(ChangeCategoriaEstadoCommand request, CancellationToken cancellationToken)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(request.IdCategoria);
            if (categoria == null)
            {
                return false;
            }
            categoria.Estado = request.Estado;
            await _repository.UpdateCategoriaAsync(categoria);
            return true;

        }
    }
}
