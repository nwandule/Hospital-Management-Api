using Hospital_Management_system.Models.Dto;
using Hospital_Management_system.Services; // Ensure this matches your AuthService namespace
using Hospital_Management_system.Services.AuthRepository;
using Microsoft.AspNetCore.Mvc;

namespace Hospital_Management_system.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto request)
        {
            var result = await _authService.RegisterPatientAsync(request);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {

            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
    }
}