using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Marcas.DTOs;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Marcas.Queries.GetMarcas
{
    public class GetMarcasHandler 
        : IRequestHandler<GetMarcasQuery, IEnumerable<MarcaResponseDTO>>
    {
        private readonly IMarcaRepository _repository;

        public GetMarcasHandler(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MarcaResponseDTO>> Handle(
            GetMarcasQuery request,
            CancellationToken cancellationToken)
        {
            var marcas = await _repository.GetMarcasAsync();

            return marcas
                //.Where(m => m.Estado)
                .Select(m => new MarcaResponseDTO
                {
                    IdMarca = m.IdMarca,
                    Nombre = m.Nombre,
                    Descripcion = m.Descripcion,
                    ColorHex = m.ColorHex,
                    Estado = m.Estado
                })
                .ToList();
        }
    }
}
