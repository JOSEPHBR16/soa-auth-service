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
using static AuthService.Application.DTOs.LoginDto;

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

        private const int MaxIntentosFallidos = 3;
        private static readonly TimeSpan DuracionBloqueo = TimeSpan.FromMinutes(5);

        // LOGIN
        public async Task<RequestResult> LoginAsync(LoginRequest request)
        {
            var persona = await _usuarioRepository.GetPersonaWithUsuarioAsync(request.Correo);
            if (persona == null || persona.Usuario == null)
                return RequestResult.WithError("Usuario o contraseña incorrectos");

            var usuario = persona.Usuario;

            if (usuario.FechaBloqueo.HasValue)
            {
                var tiempoTranscurrido = DateTime.Now - usuario.FechaBloqueo.Value;

                if (tiempoTranscurrido < DuracionBloqueo)
                {
                    var minutosRestantes = Math.Ceiling((DuracionBloqueo - tiempoTranscurrido).TotalMinutes);
                    return RequestResult.WithError($"Cuenta bloqueada temporalmente. Intenta nuevamente en {minutosRestantes} minuto(s).");
                }
                else
                {
                    usuario.FechaBloqueo = null;
                    usuario.IntentosFallidosLogin = 0;
                    await _usuarioRepository.SaveChangesAsync();
                }
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
            {
                usuario.IntentosFallidosLogin++;

                if (usuario.IntentosFallidosLogin >= MaxIntentosFallidos)
                {
                    usuario.FechaBloqueo = DateTime.Now;
                    await _usuarioRepository.SaveChangesAsync();
                    return RequestResult.WithError("Cuenta bloqueada temporalmente por exceso de intentos fallidos.");
                }

                await _usuarioRepository.SaveChangesAsync();
                return RequestResult.WithError("Usuario o contraseña incorrectos");
            }

            usuario.IntentosFallidosLogin = 0;
            usuario.FechaBloqueo = null;
            usuario.UltimoAcceso = DateTime.Now;
            await _usuarioRepository.SaveChangesAsync();

            // JWT + Refresh Token
            var accessToken = GenerateJwtToken(persona);
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                Expiration = DateTime.Now.AddDays(7),
                UsuarioID = usuario.UsuarioID,
                Revoked = false
            };

            await _usuarioRepository.AddRefreshTokenAsync(refreshToken);
            await _usuarioRepository.SaveChangesAsync();

            var response = new LoginResponse
            {
                Usuario = new UserLoginDto
                {
                    UsuarioID = usuario.UsuarioID,
                    CodigoUsuario = usuario.CodigoUsuario,
                    Nombres = persona.Nombres,
                    ApellidoPaterno = persona.ApellidoPaterno,
                    ApellidoMaterno = persona.ApellidoMaterno,
                    Email = persona.Email ?? "",
                    RolID = usuario.RolID,
                    Rol = usuario.Rol?.Nombre ?? "SinRol",
                    Telefono = persona.Telefono
                },
                Token = new TokenLoginDto
                {
                    AccessToken = accessToken,
                    ExpiresIn = 7200,
                    TokenType = "Jwt"
                },
                RefreshToken = refreshToken.Token
            };

            return RequestResult.Success(response);
        }

        // REFRESH TOKEN
        public async Task<RequestResult<LoginResponse>> RefreshTokenAsync(string refreshToken)
        {
            var tokenEntity = await _usuarioRepository.GetRefreshTokenAsync(refreshToken);
            if (tokenEntity == null || tokenEntity.Revoked || tokenEntity.Expiration < DateTime.Now)
                return RequestResult.WithError<LoginResponse>("Token inválido o expirado");

            var persona = await _usuarioRepository.GetPersonaByUsuarioIdAsync(tokenEntity.UsuarioID);
            if (persona == null)
                return RequestResult.WithError<LoginResponse>("Usuario no encontrado");

            tokenEntity.Revoked = true;
            var newToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N"),
                Expiration = DateTime.Now.AddDays(7),
                UsuarioID = tokenEntity.UsuarioID
            };

            await _usuarioRepository.AddRefreshTokenAsync(newToken);
            await _usuarioRepository.SaveChangesAsync();

            var jwt = GenerateJwtToken(persona);

            var response = new LoginResponse
            {
                Usuario = new UserLoginDto
                {
                    UsuarioID = persona.Usuario.UsuarioID,
                    CodigoUsuario = persona.Usuario.CodigoUsuario,
                    Nombres = persona.Nombres,
                    ApellidoPaterno = persona.ApellidoPaterno,
                    ApellidoMaterno = persona.ApellidoMaterno,
                    Email = persona.Email ?? "",
                    RolID = persona.Usuario.RolID,
                    Rol = persona.Usuario.Rol?.Nombre ?? "SinRol",
                    Telefono = persona.Telefono
                },
                Token = new TokenLoginDto
                {
                    AccessToken = jwt,
                    ExpiresIn = 7200,
                    TokenType = "Jwt"
                },
                RefreshToken = newToken.Token
            };

            return RequestResult.Success(response);
        }

        // LOGOUT
        public async Task<RequestResult> LogoutAsync(string refreshToken)
        {
            var tokenEntity = await _usuarioRepository.GetRefreshTokenAsync(refreshToken);
            if (tokenEntity == null)
                return RequestResult.WithError("Token no encontrado");

            tokenEntity.Revoked = true;
            await _usuarioRepository.SaveChangesAsync();

            return RequestResult.Success(true);
        }

        // REGISTRO
        public async Task<RequestResult> RegisterAsync(RegisterRequest request)
        {
            var exists = await _usuarioRepository.GetPersonaWithUsuarioAsync(request.Email);
            if (exists != null)
                return RequestResult.WithError("El correo ya está registrado");

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

        // Genera el JWT
        private string GenerateJwtToken(Persona persona)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, persona.PersonaID.ToString()),
                new Claim(ClaimTypes.Name, $"{persona.Nombres} {persona.ApellidoPaterno}"),
                new Claim(ClaimTypes.Email, persona.Email ?? ""),
                new Claim(ClaimTypes.Role, persona.Usuario?.Rol?.Nombre ?? "SinRol")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddHours(double.Parse(_config["Jwt:ExpiresHours"] ?? "2"));

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
