using AuthService.Application.Common.Response;
using AuthService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AuthService.Application.DTOs.LoginDto;

namespace AuthService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RequestResult> LoginAsync(LoginRequest request);
        Task<RequestResult<LoginResponse>> RefreshTokenAsync(string refreshToken);
        Task<RequestResult> LogoutAsync(string refreshToken);
        Task<RequestResult> RegisterAsync(RegisterRequest request);
    }
}
