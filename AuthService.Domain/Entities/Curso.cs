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
        public string CodigoCurso { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Grado { get; set; } = null!;
        public string Seccion { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Modalidad { get; set; }
        public int? CapacidadMaxima { get; set; }

        // AUDITORÍA
        public bool EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // RELACIONES
        public virtual ICollection<AsignacionDocente> AsignacionesDocentes { get; set; } = new List<AsignacionDocente>();
        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
    }
}
