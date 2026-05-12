using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Categorias.DTOs;
using Ventas.Aplicacion.Features.Productos.DTOs;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Productos.Queries.GetProductoById
{
    public class GetProductoByIdHandler
        : IRequestHandler<GetProductoByIdQuery, ProductoResponseDTO?>
    {
        private readonly IProductoRepository _repository;

        public GetProductoByIdHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductoResponseDTO?> Handle(
            GetProductoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var producto = await _repository.GetProductoByIdAsync(request.IdProducto);

            if (producto == null)
                return null;

            return new ProductoResponseDTO
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Imagen = producto.Imagen,
                PrecioVenta = producto.PrecioVenta,
                CostoPromedio = producto.CostoPromedio,
                StockActual = producto.StockActual,
                Estado = producto.Estado,
                IdMarca = producto.IdMarca,
                Marca = producto.Marca?.Nombre ?? string.Empty,

                Categorias = producto.Categorias
                    .Select(pc => new CategoriaResponseDTO
                    {
                        IdCategoria = pc.IdCategoria,
                        Nombre = pc.Categoria.Nombre,
                    })
                .ToList()
            };
        }
    }
}
