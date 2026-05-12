using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Marcas.Commands.ChangeMarcaEstado
{
    public class ChangeMarcaEstadoHandler
    : IRequestHandler<ChangeMarcaEstadoCommand, bool>
    {
        private readonly IMarcaRepository _repository;

        public ChangeMarcaEstadoHandler(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            ChangeMarcaEstadoCommand request,
            CancellationToken cancellationToken)
        {
            var marca = await _repository.GetMarcaByIdAsync(request.IdMarca);

            if (marca == null)
                throw new Exception("Marca no encontrada.");

            marca.Estado = request.Estado;

            await _repository.UpdateMarcaAsync(marca);

            return true;
        }
    }
}
