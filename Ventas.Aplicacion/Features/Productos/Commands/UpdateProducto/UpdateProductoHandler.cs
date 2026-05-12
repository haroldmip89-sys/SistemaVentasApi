using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Common.Interfaces;
using Ventas.Aplicacion.Features.Productos.DTOs;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;


namespace Ventas.Aplicacion.Features.Productos.Commands.UpdateProducto
{
    public class UpdateProductoHandler : IRequestHandler<UpdateProductoCommand, UpdateProductoResponseDTO>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IImageStorageService _imageStorageService;

        public UpdateProductoHandler(IProductoRepository productoRepository, IImageStorageService imageStorageService)
        {
            _productoRepository = productoRepository;
            _imageStorageService = imageStorageService;
        }

        public async Task<UpdateProductoResponseDTO> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            var producto = await _productoRepository.GetProductoByIdAsync(request.IdProducto);
            if (producto == null)
                throw new Exception("Producto no encontrado.");
            // Lógica de limpieza:
            if (!string.IsNullOrEmpty(request.Imagen) && !string.IsNullOrEmpty(producto.Imagen))
            {
                // Solo borramos si el usuario envió una imagen NUEVA y el producto YA TENÍA una
                await _imageStorageService.DeleteImageAsync(producto.Imagen);
            }
            producto.Nombre = request.Nombre;
            producto.Descripcion = request.Descripcion;
            producto.PrecioVenta = request.PrecioVenta;
            producto.IdMarca = request.IdMarca;
            // Solo actualizamos si se envió una ruta nueva desde el Controller
            if (!string.IsNullOrEmpty(request.Imagen))
            {
                producto.Imagen = request.Imagen;
            }
            // Actualizar categorías
            var categoriasExistentes = producto.Categorias.Select(pc => pc.IdCategoria).ToList();
            var categoriasNuevas = request.Categorias.Except(categoriasExistentes).ToList();
            var categoriasEliminar = categoriasExistentes.Except(request.Categorias).ToList();
            foreach (var idCategoria in categoriasNuevas)
            {
                producto.Categorias.Add(new ProductoCategoria { IdCategoria = idCategoria });
            }
            foreach (var idCategoria in categoriasEliminar)
            {
                var categoriaEliminar = producto.Categorias.FirstOrDefault(pc => pc.IdCategoria == idCategoria);
                if (categoriaEliminar != null)
                {
                    producto.Categorias.Remove(categoriaEliminar);
                }
            }
            await _productoRepository.UpdateProductoAsync(producto);
            return new UpdateProductoResponseDTO
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                PrecioVenta = producto.PrecioVenta,
                IdMarca = producto.IdMarca,
                Categorias = producto.Categorias.Select(pc => pc.IdCategoria).ToList()
            };
        }
    }
}
