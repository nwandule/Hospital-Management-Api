/*=============================================================================
 * Author:       Vikash
 * Description:  Data Transfer Objects (DTOs) for authentication and access control.
 * Handles incoming payloads for registration and login requests, 
 * and outgoing security token responses.
 * Created Date: June 2026
 *=============================================================================*/

namespace Hospital_Management_system.Models.Dto
{
    public class RegisterPatientDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }

    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
    }
}