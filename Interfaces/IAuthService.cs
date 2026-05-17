using TaskMini.DTO.Auth;
using TaskMini.Entities;

namespace TaskMini.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterUser(RegisterDto registerDto);

        Task<AuthResponseDto> LoginUser(LoginDto loginDto);

    }
}
