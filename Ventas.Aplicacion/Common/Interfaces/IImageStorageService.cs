using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ventas.Aplicacion.Common.Interfaces
{
    public interface IImageStorageService
    {
        Task<string?> SaveImageAsync(
            IFormFile file,
            string folder);

        Task DeleteImageAsync(string? imageUrl);
    }
}
