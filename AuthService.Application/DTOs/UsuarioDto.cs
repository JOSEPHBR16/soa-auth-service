using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class UsuarioDto
    {
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;

        public UsuarioDto() { }
        
        public UsuarioDto(Usuario usuario)
        {
            NombreCompleto = $"{usuario.Nombres} {usuario.ApellidoPaterno} {usuario.ApellidoMaterno}";
            Correo = usuario.Email;
            Rol = usuario.Rol?.Nombre ?? string.Empty;
        }
    }
}
