using MediatR;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Features.Marcas.DTOs;

namespace Ventas.Aplicacion.Features.Marcas.Queries.GetMarcaById
{
    public class GetMarcaByIdHandler
        : IRequestHandler<GetMarcaByIdQuery, MarcaResponseDTO?>
    {
        private readonly IMarcaRepository _repository;

        public GetMarcaByIdHandler(IMarcaRepository repository)
        {
            _repository = repository;
        }

        public async Task<MarcaResponseDTO?> Handle(
            GetMarcaByIdQuery request,
            CancellationToken cancellationToken)
        {
            var marca = await _repository.GetMarcaByIdAsync(request.IdMarca);

            if (marca == null || !marca.Estado)
                return null;

            return new MarcaResponseDTO
            {
                IdMarca = marca.IdMarca,
                Nombre = marca.Nombre,
                Descripcion = marca.Descripcion,
                ColorHex = marca.ColorHex,
                Estado = marca.Estado
            };
        }
    }
}
