using AuthService.Application.Common.Response;
using AuthService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<RequestResult<List<UsuarioDto>>> GetAllAsync();
    }
}
