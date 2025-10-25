using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.DTOs
{
    public class RegisterRequest
    {
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RolID { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
    }
}
