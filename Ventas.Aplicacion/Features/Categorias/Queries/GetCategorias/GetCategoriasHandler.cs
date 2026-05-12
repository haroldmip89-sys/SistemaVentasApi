using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Features.Categorias.DTOs;

namespace Ventas.Aplicacion.Features.Categorias.Queries.GetCategorias
{
    public class GetCategoriasHandler
        : IRequestHandler<GetCategoriasQuery, IEnumerable<CategoriaResponseDTO>>
    {
        private readonly ICategoriaRepository _repository;

        public GetCategoriasHandler(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoriaResponseDTO>> Handle(
            GetCategoriasQuery request,
            CancellationToken cancellationToken)
        {
            var categorias = await _repository.GetCategoriasAsync();

            return categorias
                //.Where(c => c.Estado) // para devolver solo los activos descomentar esta linea
                .Select(c => new CategoriaResponseDTO
                {
                    IdCategoria = c.IdCategoria,
                    Nombre = c.Nombre,
                    Descripcion = c.Descripcion,
                    ColorHex = c.ColorHex,
                    Estado = c.Estado
                })
                .ToList();
        }
    }
}
