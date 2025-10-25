using AuthService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Persona>> GetAllAsync();
        Task<Persona?> GetByEmailAsync(string correo);
        Task<Persona?> GetPersonaWithUsuarioAsync(string email);
        Task<Persona?> GetPersonaByUsuarioIdAsync(int usuarioId);
        Task AddAsync(Persona persona);
        Task AddRefreshTokenAsync(RefreshToken token);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task SaveChangesAsync();
    }
}
