using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string CodigoCurso { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Grado { get; set; } = string.Empty;
        public string Seccion { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public string? Modalidad { get; set; }
        public int? CapacidadMaxima { get; set; }
        public int DocenteID { get; set; }
        public bool? EstadoRegistro { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // 🔗 Relaciones
        public Usuario? Docente { get; set; }
        public ICollection<Matricula>? Matriculas { get; set; }
    }
}
