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

        public async Task<List<Persona>> GetAllAsync()
        {
            //return await _context.Usuarios.Include(u => u.Rol).ToListAsync();
            return await _context.Personas.ToListAsync();
        }

        public async Task AddAsync(Persona persona)
        {
            await _context.Personas.AddAsync(persona);
        }

        public async Task<Persona?> GetByEmailAsync(string correo)
        {
            return await _context.Personas.FirstOrDefaultAsync(u => u.Email == correo && u.EstadoRegistro == true);
        }

        public async Task<Persona?> GetPersonaWithUsuarioAsync(string email)
        {
            return await _context.Personas
                .Include(p => p.Usuario)
                    .ThenInclude(u => u.Rol)
                .FirstOrDefaultAsync(p => p.Email == email && p.EstadoRegistro);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
