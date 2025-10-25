using AuthService.Application.Common.Response;
using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


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
            var persona = await _usuarioRepository.GetPersonaWithUsuarioAsync(correo);
            if (persona == null || persona.Usuario == null)
                return RequestResult.WithError<string>("Usuario o contraseña incorrectos");

            bool passwordValid = BCrypt.Net.BCrypt.Verify(password, persona.Usuario.PasswordHash);
            if (!passwordValid)
                return RequestResult.WithError<string>("Usuario o contraseña incorrectos");

            var token = GenerateJwtToken(persona);
            return RequestResult.Success(token);
        }

        // 🧾 REGISTRO
        public async Task<RequestResult<bool>> RegisterAsync(RegisterRequest request)
        {
            var exists = await _usuarioRepository.GetPersonaWithUsuarioAsync(request.Email);
            if (exists != null)
                return RequestResult.WithError<bool>("El correo ya está registrado");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var persona = new Persona
            {
                CodigoPersona = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                Nombres = request.Nombres,
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                Email = request.Email,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.NumeroDocumento,
                //RolID = rol.RolID,
                FechaHoraCreacion = DateTime.Now,
                EstadoRegistro = true
            };

            var usuario = new Usuario
            {
                CodigoUsuario = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(),
                PasswordHash = hashedPassword,
                RolID = request.RolID,
                FechaHoraCreacion = DateTime.Now,
                EstadoRegistro = true
            };

            persona.Usuario = usuario;

            await _usuarioRepository.AddAsync(persona);
            await _usuarioRepository.SaveChangesAsync();

            return RequestResult.Success(true);
        }

        // ⚙️ Genera el JWT
        private string GenerateJwtToken(Persona persona)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, persona.PersonaID.ToString()),
                new Claim(ClaimTypes.Name, $"{persona.Nombres} {persona.ApellidoPaterno}"),
                new Claim(ClaimTypes.Email, persona.Email ?? ""),
                new Claim(ClaimTypes.Role, persona.Usuario?.Rol?.Nombre ?? "SinRol")
            };

            // Obtener clave desde el Key Vault (ya cargada en _config)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(2);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
