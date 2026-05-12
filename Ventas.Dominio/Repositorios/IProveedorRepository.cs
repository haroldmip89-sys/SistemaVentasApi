using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Interfaces
{
    public interface IProveedorRepository
    {
        //Definir los metodos para el repositorio de proveedores
        Task<bool> ExisteProveedorAsync(string nombre);
        Task<Proveedor> AddProveedorAsync(Proveedor proveedor);
        Task<Proveedor?> GetProveedorByIdAsync(int id);
        Task<IEnumerable<Proveedor>> GetProveedoresAsync();
        Task<Proveedor> UpdateProveedorAsync(Proveedor proveedor);
        //no implementar en aplicacion(soft delete)
        Task<bool> DeleteProveedorAsync(int id);
    }
}
