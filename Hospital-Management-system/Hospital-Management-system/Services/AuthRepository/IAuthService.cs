/*=============================================================================
 * Author:       Vikash
 * Description:  Interface defining contract operations for user authentication,
 * session token generation, and account registration.
 * Created Date: June 2026
 *=============================================================================*/

/*=============================================================================
 * Author:       Vikash
 * Description:  Interface defining contract operations for user authentication,
 * session token generation, and account registration.
 * Created Date: June 2026
 *=============================================================================*/
using Hospital_Management_system.Models.Dto;

namespace Hospital_Management_system.Services.AuthRepository
{
    public interface IAuthService
    {
        Task<string> RegisterPatientAsync(RegisterPatientDto dto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
