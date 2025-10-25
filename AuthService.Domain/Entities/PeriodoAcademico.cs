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
        public string NombrePeriodo { get; set; } = string.Empty;
        public int Anio { get; set; }
        public int Trimestre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime? FechaCierreNotas { get; set; }
        public string Estado { get; set; } = "Activo";
        public string? Observaciones { get; set; }
        public bool? EstadoRegistro { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime? FechaHoraCreacion { get; set; }
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // 🔗 Relaciones
        public ICollection<Matricula>? Matriculas { get; set; }
        public ICollection<Nota>? Notas { get; set; }
    }
}
