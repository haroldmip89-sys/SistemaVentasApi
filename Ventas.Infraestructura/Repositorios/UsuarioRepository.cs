using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Interfaces;
using Microsoft.EntityFrameworkCore;
using Ventas.Infraestructura.Context;

namespace Ventas.Infraestructura.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> AddUsuarioAsync(Usuario usuario)
        {
            var result = await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        //NO IMPLEMENTAR EN APLICACION
        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario =  await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> LoginAsync(string email, string passwordhash)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Email == email &&
                    x.PasswordHash == passwordhash &&
                    x.Estado == true);
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
        //para verificar si existe el email usamos AnyAsync, solo devuelve true o false
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(x => x.Email == email);
        }
    }
}
