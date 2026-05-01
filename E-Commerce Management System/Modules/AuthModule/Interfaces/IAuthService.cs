using E_Commerce_Management_System.Modules.AuthModule.Dtos;
using E_Commerce_Management_System.Shared.Helpers;

namespace E_Commerce_Management_System.Modules.AuthModule.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDto>> RegisterCustomerAsync(RegisterRequestDto request);
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto request);
    }
}
