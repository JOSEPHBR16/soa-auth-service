using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class Nota
    {
        public int NotaID { get; set; }
        public int MatriculaID { get; set; }
        public int PeriodoID { get; set; }
        public decimal? ValorNota { get; set; }
        public string? ObservacionDocente { get; set; }
        public DateTime? FechaEvaluacion { get; set; }
        public int DocenteID { get; set; }

        // AUDITORÍA
        public bool? EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // RELACIONES
        public virtual Matricula Matricula { get; set; } = null!;
        public virtual PeriodoAcademico Periodo { get; set; } = null!;
        public virtual Usuario Docente { get; set; } = null!;
    }
}
