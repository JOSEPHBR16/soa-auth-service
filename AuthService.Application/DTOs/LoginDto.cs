using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class LoginDto
    {
        public class LoginRequest
        {
            public string Correo { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class LoginResponse
        {
            public UserLoginDto Usuario { get; set; } = default!;
            public TokenLoginDto Token { get; set; } = default!;
            public string? RefreshToken { get; set; }
            //public string? ResetPasswordUrl { get; set; }
        }

        public class UserLoginDto
        {
            public int UsuarioID { get; set; } = default!;
            public string CodigoUsuario { get; set; } = default!;
            public string Nombres { get; set; } = default!;
            public string ApellidoPaterno { get; set; } = default!;
            public string ApellidoMaterno { get; set; } = default!;
            public string Email { get; set; } = default!;
            public int RolID { get; set; } = default!;
            public string Rol { get; set; } = default!;
            public string Telefono { get; set; } = default!;
        }

        public class TokenLoginDto
        {
            public string AccessToken { get; set; } = default!;
            public int ExpiresIn { get; set; }
            public string TokenType { get; set; } = "Jwt";
        }
    }
}
