using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ventas.Aplicacion.Modelos;
using Ventas.Aplicacion.Services;

namespace Ventas.Infraestructura.Services
{
    public class TokenService : ITokenService
    {
        public const string Secret = "Cibertec9669ass969dd9ffg6g9hh935434235433";
        private const double expireHours = 1.0;
        public string CreateToken(Usuario usuario)
        {
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
            new Claim(ClaimTypes.Email, usuario.Email.ToString()),
            new Claim(ClaimTypes.Role, usuario.Rol.ToString()) // CLAVE
        };

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(expireHours),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
