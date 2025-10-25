using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class x_Usuario
    {
        public int UsuarioID { get; set; }
        public string CodigoUsuario { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Celular { get; set; }
        public char? Genero { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? FotoPerfilUrl { get; set; }
        public string? Cargo { get; set; }
        public string? EstadoCivil { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public DateTime? FechaCese { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? UltimoAcceso { get; set; }
        public int IntentosFallidosLogin { get; set; }
        public DateTime? FechaBloqueo { get; set; }
        public bool EsExterno { get; set; }
        public int RolID { get; set; }
        public bool? EstadoRegistro { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // 🔗 Relaciones
        public Rol? Rol { get; set; }
        //public ICollection<Curso>? CursosDictados { get; set; }
        //public ICollection<Matricula>? Matriculas { get; set; }
        //public ICollection<Nota>? NotasDocente { get; set; }
        //public ICollection<PadreAlumno>? PadresAlumnosPadre { get; set; }
        //public ICollection<PadreAlumno>? PadresAlumnosHijo { get; set; }
    }
}
