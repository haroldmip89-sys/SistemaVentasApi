using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Marcas.Commands.CreateMarca
{
    public class CreateMarcaHandler
    : IRequestHandler<CreateMarcaCommand, int>
    {
        private readonly IMarcaRepository _repository;

        public CreateMarcaHandler(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(
            CreateMarcaCommand request,
            CancellationToken cancellationToken)
        {
            var marca = new Marca
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                ColorHex = request.ColorHex,
                Estado = true
            };

            await _repository.AddMarcaAsync(marca);

            return marca.IdMarca;
        }
    }
}
