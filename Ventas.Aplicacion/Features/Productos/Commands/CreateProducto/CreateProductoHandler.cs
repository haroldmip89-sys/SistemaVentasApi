using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Features.Productos.DTOs;
using Ventas.Aplicacion.Interfaces;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Features.Productos.Commands.CreateProducto
{
    public class CreateProductoHandler:IRequestHandler<CreateProductoCommand,CreateProductoResponseDTO>
    {
        private readonly IProductoRepository _productoRepository;
        public CreateProductoHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }
        public async Task<CreateProductoResponseDTO> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = new Producto
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                Imagen = request.Imagen,
                IdMarca = request.IdMarca,
                PrecioVenta = request.PrecioVenta,
                StockActual = 0,
                CostoPromedio = 0,
                Estado = true,
                Categorias = request.Categorias.Select(c => new ProductoCategoria { IdCategoria = c }).ToList()
            };
            var createdProducto = await _productoRepository.AddProductoAsync(producto);
            return new CreateProductoResponseDTO
            {
                IdProducto = createdProducto.IdProducto,
                Nombre = createdProducto.Nombre,
                Descripcion = createdProducto.Descripcion,
                IdMarca = createdProducto.IdMarca,
                PrecioVenta = createdProducto.PrecioVenta,
                Categorias = createdProducto.Categorias.Select(c => c.IdCategoria).ToList()
            };

        }
    }
}
