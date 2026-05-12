using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Features.Categorias.DTOs;

namespace Ventas.Aplicacion.Features.Categorias.Queries.GetCategoriaById
{
    public class GetCategoriaByIdHandler
        : IRequestHandler<GetCategoriaByIdQuery, CategoriaResponseDTO?>
    {
        private readonly ICategoriaRepository _repository;

        public GetCategoriaByIdHandler(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoriaResponseDTO?> Handle(
            GetCategoriaByIdQuery request,
            CancellationToken cancellationToken)
        {
            var categoria = await _repository.GetCategoriaByIdAsync(request.Id);

            if (categoria == null || !categoria.Estado)
                return null;

            return new CategoriaResponseDTO
            {
                IdCategoria = categoria.IdCategoria,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                ColorHex = categoria.ColorHex,
                Estado = categoria.Estado
            };
        }
    }
}
