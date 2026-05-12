using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Categorias.Commands.UpdateCategoria
{
    public class UpdateCategoriaHandler:IRequestHandler<UpdateCategoriaCommand,bool>
    {
        private readonly ICategoriaRepository _repository;
        public UpdateCategoriaHandler(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(request.IdCategoria);
            if (categoria == null)
            {
                return false;
            }

            categoria.Nombre = request.Nombre;
            categoria.Descripcion = request.Descripcion;
            categoria.ColorHex = request.ColorHex;

            await _repository.UpdateCategoriaAsync(categoria);
            return true;
        }
    }
}
