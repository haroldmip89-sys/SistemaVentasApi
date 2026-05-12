using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Interfaces
{
    public interface ICategoriaRepository
    {
        //Definir los metodos para el repositorio de marcas
        Task<Categoria> AddCategoriaAsync(Categoria categoria);
        Task<Categoria?> GetCategoriaByIdAsync(int id);
        Task<IEnumerable<Categoria>> GetCategoriasAsync();
        Task<Categoria> UpdateCategoriaAsync(Categoria categoria);
        //no implementar en aplicacion(soft delete)
        Task<bool> DeleteCategoriaAsync(int id);
    }
}
