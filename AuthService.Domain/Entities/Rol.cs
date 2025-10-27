using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class Rol
    {
        public int RolID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }

        // AUDITORÍA
        public bool? EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // RELACIONES}
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
