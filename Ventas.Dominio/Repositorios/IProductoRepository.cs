using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Interfaces
{
    public interface IProductoRepository
    {
        //Definir los metodos para el repositorio de productos
        Task<Producto> AddProductoAsync(Producto producto);
        Task<Producto?> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetProductosAsync();
        Task<Producto> UpdateProductoAsync(Producto producto);

        //no implementar en aplicacion(soft delete)
        Task<bool> DeleteProductoAsync(int id);
        Task<(IEnumerable<Producto> Items, int Total)> GetProductosPagedAsync(
        int page,
        int pageSize,
        string? search,
        int? categoriaId,
        int? marcaId,
        string? orderBy,
        string? orderDir,
        bool soloActivos = true
    );

    }
}
