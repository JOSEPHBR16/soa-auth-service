using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class PadreAlumno
    {
        public int PadreAlumnoID { get; set; }
        public int PadreID { get; set; }
        public int AlumnoID { get; set; }
        public string Parentesco { get; set; } = string.Empty;
        public string? TelefonoEmergencia { get; set; }
        public string? Direccion { get; set; }
        public string? EmailContacto { get; set; }
        public string EstadoRelacion { get; set; } = "Activa";
        public DateTime? FechaRegistro { get; set; }
        public bool? EstadoRegistro { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // 🔗 Relaciones
        public Usuario? Padre { get; set; }
        public Usuario? Alumno { get; set; }
    }
}
