using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;
using Ventas.Infraestructura.Context;
using Microsoft.EntityFrameworkCore;

namespace Ventas.Infraestructura.Repositorios
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly ApplicationDbContext _context;

        public MarcaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Marca> AddMarcaAsync(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return marca;
        }

        public async Task<Marca?> GetMarcaByIdAsync(int id)
        {
            return await _context.Marcas
                .FirstOrDefaultAsync(p => p.IdMarca == id);
        }

        public async Task<IEnumerable<Marca>> GetMarcasAsync()
        {
            return await _context.Marcas.ToListAsync();
        }

        public async Task<Marca> UpdateMarcaAsync(Marca marca)
        {
            _context.Marcas.Update(marca);
            await _context.SaveChangesAsync();
            return marca;
        }

        public async Task<bool> DeleteMarcaAsync(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return false;
            }
            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
