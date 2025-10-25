using AuthService.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request.Correo, request.Password);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Application.DTOs.RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }

        public class LoginRequest
        {
            public string Correo { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        //public class RegisterRequest
        //{
        //    public string Nombres { get; set; } = string.Empty;
        //    public string ApellidoPaterno { get; set; } = string.Empty;
        //    public string ApellidoMaterno { get; set; } = string.Empty;
        //    public string Email { get; set; } = string.Empty;
        //    public string Password { get; set; } = string.Empty;
        //    public string Rol { get; set; } = "Alumno";
        //}
    }
}
