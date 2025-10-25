using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class Matricula
    {
        public int MatriculaID { get; set; }
        public int AlumnoID { get; set; }
        public int CursoID { get; set; }
        public int PeriodoID { get; set; }
        public string CodigoMatricula { get; set; } = string.Empty;
        public DateTime? FechaMatricula { get; set; }
        public string EstadoMatricula { get; set; } = "Activa";
        public string? Observaciones { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public bool? EstadoRegistro { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // 🔗 Relaciones
        public Usuario? Alumno { get; set; }
        public Curso? Curso { get; set; }
        public PeriodoAcademico? Periodo { get; set; }
        public ICollection<Nota>? Notas { get; set; }
    }
}
