using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;

namespace Ventas.Aplicacion.Interfaces
{
    public interface IUsuarioRepository
    {
        //Login: TODOS
        Task<Usuario?> LoginAsync(string email, string passwordhash);
        //Crud: ADMIN
        Task<Usuario> AddUsuarioAsync(Usuario usuario);
        Task<IEnumerable<Usuario>> GetUsuariosAsync();
        Task<Usuario?> GetUsuarioByIdAsync(int id);
        Task<Usuario> UpdateUsuarioAsync(Usuario usuario);
        //No usar Delete, solo para pruebas
        Task<bool> DeleteUsuarioAsync(int id);
        //verificar si el email ya existe
        Task<bool> EmailExistsAsync(string email);
    }
}
