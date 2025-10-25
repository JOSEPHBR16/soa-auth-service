using AuthService.Application.Common.Response;
using AuthService.Application.DTOs;
using AuthService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<RequestResult<List<UsuarioDto>>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return RequestResult.Success(users.Select(u => new UsuarioDto(u)).ToList());
        }
    }
}
