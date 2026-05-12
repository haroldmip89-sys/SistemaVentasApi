using Ventas.Infraestructura.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ventas.Infraestructura.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Producto> AddProductoAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<Producto?> GetProductoByIdAsync(int id)
        {
            return await _context.Productos.Include(p => p.Categorias).ThenInclude(pc => pc.Categoria).Include(p => p.Marca)
                .FirstOrDefaultAsync(p => p.IdProducto == id);
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            return await _context.Productos.Include(p => p.Marca).Include(p => p.Categorias).ToListAsync();
        }

        public async Task<Producto> UpdateProductoAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
            return producto;
        }

        public async Task<bool> DeleteProductoAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return false;
            }
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<(IEnumerable<Producto> Items, int Total)> GetProductosPagedAsync(
            int page,
            int pageSize,
            string? search,
            int? idCategoria,
            int? idMarca,
            string? orderBy,
            string? orderDir,
            bool soloActivos)
        {
            var query = _context.Productos
                .AsNoTracking()
                .Include(p => p.Marca) // ESTO ES CLAVE
                .Include(p => p.Categorias)
                .ThenInclude(pc => pc.Categoria)
                .AsQueryable();

            // FILTRO DINÁMICO DE ESTADO
            if (soloActivos)
            {
                query = query.Where(p => p.Estado == true);
            }
            //  BÚSQUEDA
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p =>
                    p.Nombre.Contains(search));
            }

            //  FILTRO CATEGORÍA (muchos a muchos)
            if (idCategoria.HasValue)
            {
                query = query.Where(p =>
                    p.Categorias.Any(pc => pc.IdCategoria == idCategoria));
            }

            //  FILTRO MARCA
            if (idMarca.HasValue)
            {
                query = query.Where(p => p.IdMarca == idMarca);
            }

            // ↕ ORDENAMIENTO
            query = orderBy?.ToLower() switch
            {
                "precio" => orderDir == "desc"
                    ? query.OrderByDescending(p => p.PrecioVenta)
                    : query.OrderBy(p => p.PrecioVenta),

                "nombre" => orderDir == "desc"
                    ? query.OrderByDescending(p => p.Nombre)
                    : query.OrderBy(p => p.Nombre),

                _ => query.OrderBy(p => p.Nombre)
            };

            var total = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, total);
        }

    }
}
