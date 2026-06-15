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
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto request)
        {
            _logger.LogInformation("Processing registration request for email: {Email}", request.Email);
            var result = await _authService.RegisterPatientAsync(request);
            _logger.LogInformation("Patient registered successfully with email: {Email}", request.Email);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {

            _logger.LogInformation("Login attempt received for email: {Email}", request.Email);

            var response = await _authService.LoginAsync(request);

            if (response == null)
            {
                _logger.LogWarning("Failed login attempt for email: {Email}. Invalid credentials.", request.Email);
                return Unauthorized(new { message = "Invalid email or password." });
            }
            _logger.LogInformation("User logged in successfully: {Email}", request.Email);
            return Ok(response);
        }
    }
}