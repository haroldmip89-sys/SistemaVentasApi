using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Categorias.DTOs;
using Ventas.Aplicacion.Features.Productos.DTOs;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;

namespace Ventas.Aplicacion.Features.Productos.Queries.GetProductosPaged
{
    public class GetProductosPagedHandler
    : IRequestHandler<GetProductosPagedQuery, PagedResultResponseDTO<ProductoResponseDTO>>
    {
        private readonly IProductoRepository _repository;

        public GetProductosPagedHandler(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResultResponseDTO<ProductoResponseDTO>> Handle(
            GetProductosPagedQuery request,
            CancellationToken cancellationToken)
        {
            // 1. El repositorio filtra qué productos regresan de la DB
            var (productos, total) = await _repository.GetProductosPagedAsync(
                request.Page,
                request.PageSize,
                request.Search,
                request.idCategoria,
                request.idMarca,
                request.OrderBy,
                request.OrderDir,
                request.soloActivos
            );
            var productosDto = productos.Select(producto => new ProductoResponseDTO
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
                Marca = producto.Marca != null ? producto.Marca.Nombre : string.Empty,

                // 2. Aquí aplicamos la lógica dinámica para las categorías
                Categorias = producto.Categorias
                .Where(pc => request.soloActivos ? pc.Categoria.Estado : true)
                    /* Explicación:
                       - Si soloActivos es TRUE: Solo incluimos en el DTO las categorías activas.
                       - Si soloActivos es FALSE (Inventario): Mostramos todas, incluso las inactivas.
                    */
                    .Select(pc => new CategoriaResponseDTO
                    {
                        IdCategoria = pc.IdCategoria,
                        Nombre = pc.Categoria.Nombre,
                        ColorHex = pc.Categoria.ColorHex
                    })
        .ToList()
            }).ToList();

            return new PagedResultResponseDTO<ProductoResponseDTO>
            {
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total,
                Productos = productosDto
            };
        }
    }
}
