using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;
using Ventas.Infraestructura.Context;

namespace Ventas.Infraestructura.Repositorios
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly ApplicationDbContext _context;

        public ProveedorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Proveedor> AddProveedorAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }

        public async Task<bool> DeleteProveedorAsync(int id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return false;
            }
            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExisteProveedorAsync(string nombre)
        {
            return await _context.Proveedores
                .AnyAsync(p => p.RazonSocial == nombre);
        }

        public async Task<Proveedor?> GetProveedorByIdAsync(int id)
        {
            return await _context.Proveedores.FindAsync(id);
        }

        public async Task<IEnumerable<Proveedor>> GetProveedoresAsync()
        {
            return await _context.Proveedores.ToListAsync();
        }

        public async Task<Proveedor> UpdateProveedorAsync(Proveedor proveedor)
        {
            _context.Proveedores.Update(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
    }
}
