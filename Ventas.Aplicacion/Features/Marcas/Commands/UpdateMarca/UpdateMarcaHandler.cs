using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Marcas.Commands.UpdateMarca
{
    public class UpdateMarcaHandler
    : IRequestHandler<UpdateMarcaCommand, bool>
    {
        private readonly IMarcaRepository _repository;

        public UpdateMarcaHandler(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            UpdateMarcaCommand request,
            CancellationToken cancellationToken)
        {
            var marca = await _repository.GetMarcaByIdAsync(request.IdMarca);

            if (marca == null)
                throw new Exception("Marca no encontrada.");

            marca.Nombre = request.Nombre;
            marca.Descripcion = request.Descripcion;
            marca.ColorHex = request.ColorHex;

            await _repository.UpdateMarcaAsync(marca);

            return true;
        }
    }
}
