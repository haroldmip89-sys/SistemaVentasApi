using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Interfaces
{
    public interface IMarcaRepository
    {
        //Definir los metodos para el repositorio de marcas
        Task<Marca> AddMarcaAsync(Marca marca);
        Task<Marca?> GetMarcaByIdAsync(int id);
        Task<IEnumerable<Marca>> GetMarcasAsync();
        Task<Marca> UpdateMarcaAsync(Marca marca);
        //no implementar en aplicacion(soft delete)
        Task<bool> DeleteMarcaAsync(int id);
    }
}
