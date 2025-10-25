using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using AuthService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AuthDbContext _context;

        public UsuarioRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            //return await _context.Usuarios.Include(u => u.Rol).ToListAsync();
            return await _context.Usuarios.ToListAsync();
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task<Usuario?> GetByEmailAsync(string correo)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == correo && u.EstadoRegistro == true);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
