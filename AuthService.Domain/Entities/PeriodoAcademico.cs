using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class PeriodoAcademico
    {
        public int PeriodoID { get; set; }
        public string NombrePeriodo { get; set; } = null!;
        public int Anio { get; set; }
        public int Trimestre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaCierreNotas { get; set; }
        public string Estado { get; set; } = "Activo";
        public string? Observaciones { get; set; }

        // AUDITORÍA
        public bool EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // RELACIONES
        public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
        public virtual ICollection<AsignacionDocente> AsignacionesDocentes { get; set; } = new List<AsignacionDocente>();
        public virtual ICollection<Nota> Notas { get; set; } = new List<Nota>();
    }
}
