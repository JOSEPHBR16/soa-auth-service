using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class Persona
    {
        public int PersonaID { get; set; }
        public string CodigoPersona { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string TipoDocumento { get; set; } = null!;
        public string NumeroDocumento { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public char? Genero { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public string? Direccion { get; set; }
        public string? FotoPerfilUrl { get; set; }

        // Auditoría
        public bool EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // Relación 1:1 con Usuario
        public virtual Usuario? Usuario { get; set; }
    }
}
