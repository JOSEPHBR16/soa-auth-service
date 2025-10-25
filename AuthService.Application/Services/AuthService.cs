using AuthService.Application.Common.Response;
using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _config;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _config = config;
        }

        // 🔐 LOGIN
        public async Task<RequestResult<string>> LoginAsync(string correo, string password)
        {
            var usuario = await _usuarioRepository.GetByEmailAsync(correo);
            if (usuario == null || !(password == usuario.PasswordHash))
                return RequestResult.WithError<string>("Usuario o contraseña incorrectos");

            var token = GenerateJwtToken(usuario);
            return RequestResult.Success(token);
        }

        // 🧾 REGISTRO
        public async Task<RequestResult<bool>> RegisterAsync(RegisterRequest request)
        {
            var exists = await _usuarioRepository.GetByEmailAsync(request.Email);
            if (exists != null) return RequestResult.WithError<bool>("El usuario ya existe");

            //var rol = await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == request.Rol);
            //if (rol == null) return false;

            var usuario = new Usuario
            {
                CodigoUsuario = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                Nombres = request.Nombres,
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                Email = request.Email,
                PasswordHash = request.Password, // En producción: usar hash real
                //RolID = rol.RolID,
                FechaHoraCreacion = DateTime.Now,
                EstadoRegistro = true
            };

            await _usuarioRepository.AddAsync(usuario);
            await _usuarioRepository.SaveChangesAsync();

            return RequestResult.Success(true);
        }

        // ⚙️ Genera el JWT
        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                //new Claim(ClaimTypes.Role, usuario.Rol)
            };

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddHours(2), signingCredentials: creds);
            //return new JwtSecurityTokenHandler().WriteToken(token);
            return "";
        }
    }
}
