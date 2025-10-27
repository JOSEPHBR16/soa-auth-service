using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Domain.Entities
{
    public class AsignacionDocente
    {
        public int AsignacionID { get; set; }
        public int CursoID { get; set; }
        public int DocenteID { get; set; }
        public int PeriodoID { get; set; }
        public string? RolDocente { get; set; }
        public string? Observaciones { get; set; }

        // AUDITORÍA
        public bool EstadoRegistro { get; set; } = true;
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaHoraCreacion { get; set; } = DateTime.Now;
        public string? UsuarioActualizacion { get; set; }
        public DateTime? FechaHoraActualizacion { get; set; }

        // RELACIONES
        public virtual Curso Curso { get; set; } = null!;
        public virtual Usuario Docente { get; set; } = null!;
        public virtual PeriodoAcademico Periodo { get; set; } = null!;
    }
}
