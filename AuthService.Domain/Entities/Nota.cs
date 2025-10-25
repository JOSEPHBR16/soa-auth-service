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
        public int UsuarioDocenteID { get; set; }
        public bool? EstadoRegistro { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // 🔗 Relaciones
        public Matricula? Matricula { get; set; }
        public PeriodoAcademico? Periodo { get; set; }
        public Usuario? Docente { get; set; }
    }
}
