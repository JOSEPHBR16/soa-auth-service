using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public int PersonaID { get; set; }
        public string CodigoUsuario { get; set; } = null!;
        public int RolID { get; set; }
        public string PasswordHash { get; set; } = null!;
        public DateTime? UltimoAcceso { get; set; }
        public int IntentosFallidosLogin { get; set; } = 0;
        public DateTime? FechaBloqueo { get; set; }
        public bool EsExterno { get; set; } = false;
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaCese { get; set; }

        // Auditoría
        public bool EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // Relaciones
        public virtual Persona Persona { get; set; } = null!;
        public virtual Rol Rol { get; set; } = null!;
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
