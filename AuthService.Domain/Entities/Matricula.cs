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
        public string CodigoMatricula { get; set; } = null!;
        public DateTime? FechaMatricula { get; set; } = DateTime.Now;
        public string EstadoMatricula { get; set; } = "Activa";
        public string? Observaciones { get; set; }
        public DateTime? FechaRetiro { get; set; }

        // AUDITORÍA
        public bool EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // RELACIONES
        public virtual Usuario Alumno { get; set; } = null!;
        public virtual Curso Curso { get; set; } = null!;
        public virtual PeriodoAcademico Periodo { get; set; } = null!;
        public virtual ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
