using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Common.Interfaces;
using System.IO;

namespace Ventas.Infraestructura.Services
{
    public class ImageStorageService : IImageStorageService
    {
        private readonly string _rootPath;
        // colocar <FrameworkReference Include="Microsoft.AspNetCore.App" /> en el .csproj para acceder a IWebHostEnvironment
        public ImageStorageService(IWebHostEnvironment env)
        {
            _rootPath = env.WebRootPath;
        }

        public async Task<string?> SaveImageAsync(IFormFile file, string folder)
        {
            if (file == null) return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/webp" };
            const long maxFileSize = 2 * 1024 * 1024;

            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(extension) ||
                !allowedMimeTypes.Contains(file.ContentType))
                throw new Exception("Formato de imagen no permitido");

            if (file.Length > maxFileSize)
                throw new Exception("La imagen excede 2MB");

            var uploadsPath = Path.Combine(_rootPath, "uploads", folder);

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{folder}/{fileName}";
        }

        public Task DeleteImageAsync(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
                return Task.CompletedTask;

            var path = Path.Combine(
                _rootPath,
                imageUrl.TrimStart('/')
            );

            if (File.Exists(path))
                File.Delete(path);

            return Task.CompletedTask;
        }
    }
}
